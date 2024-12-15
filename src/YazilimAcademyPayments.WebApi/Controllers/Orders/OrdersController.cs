using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using YazilimAcademyPayments.WebApi.Controllers.Orders.Models;
using YazilimAcademyPayments.WebApi.Domain.Entities;
using YazilimAcademyPayments.WebApi.Domain.Helpers;
using YazilimAcademyPayments.WebApi.Extensions;
using YazilimAcademyPayments.WebApi.Persistence.EntityFramework.Contexts;
using YazilimAcademyPayments.WebApi.Settings;

namespace YazilimAcademyPayments.WebApi.Controllers.Orders;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = "ApiKeyScheme")]
public sealed class OrdersController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly PayTRSettings _paytrSettings;
    private readonly IWebHostEnvironment _env;

    public OrdersController(ApplicationDbContext context, IHttpClientFactory httpClientFactory, IOptions<PayTRSettings> paytrSettings, IWebHostEnvironment env)
    {
        _context = context;
        _httpClientFactory = httpClientFactory;
        _paytrSettings = paytrSettings.Value;
        _env = env;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var tenantId = HttpContext.User.GetTenantId();

        var orders = await _context
            .Orders
            .AsNoTracking()
            .Where(o => o.TenantId == tenantId)
            .ToListAsync(cancellationToken);

        return Ok(orders);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var tenantId = HttpContext.User.GetTenantId();

        var user = await _context
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.TenantId == tenantId && u.Email == request.User.Email, cancellationToken);

        if (user is null)
        {
            user = CreateOrderRequestUserDto.From(request.User, tenantId);
            _context.Users.Add(user);
        }

        var order = CreateOrderRequest.ToOrder(request, tenantId, user.Id);
        _context.Orders.Add(order);
        await _context.SaveChangesAsync(cancellationToken);

        return Ok();
    }

    // Yeni bir endpoint: PayTR IFrame token oluşturma
    [HttpPost("get-paytr-token")]
    public async Task<IActionResult> GetPayTRTokenAsync(CreateOrderRequest request, User user, CancellationToken cancellationToken)
    {
        // Bu değerleri konfigürasyondan okuyun
        var merchantId = _paytrSettings.MerchantId;
        var merchantKey = _paytrSettings.MerchantKey;
        var merchantSalt = _paytrSettings.MerchantSalt;

        if (string.IsNullOrEmpty(merchantId) || string.IsNullOrEmpty(merchantKey) || string.IsNullOrEmpty(merchantSalt))
            return StatusCode(500, "PayTR yapılandırması eksik.");

        var tenantId = HttpContext.User.GetTenantId();

        var tenant = await _context
            .Tenants
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == tenantId, cancellationToken);

        var product = await _context
            .Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);

        // PayTR'ın istediği formatta tutar: Örn. 9.99 için 999 gönderilmeli.
        int paymentAmountKurus = (int)(request.PaymentAmount * 100);

        // Sipariş numarası (merchant_oid): request.MerchantOid
        var merchantOid = request.MerchantOid;

        // Kullanıcı bilgileri
        var email = user.Email;
        var fullName = user.FullName;
        var address = request.User.Address;
        var phoneNumber = user.PhoneNumber;

        // Başarılı/başarısız dönüş URL'leri
        var merchantOkUrl = tenant.SuccessPath;
        var merchantFailUrl = tenant.FailPath;

        // Kullanıcı IP alma
        var userIp = GetIpAddress();

        var quantity = 1;

        // Sepet içeriği (Örnek tek ürün)
        // Burada request'ten gelen product bilgilerini kullanabilir, birim fiyat ve adet belirtebilirsiniz.
        // PayTR her ürün için ad, fiyat ve adet bekliyor.
        // Bu örnekte PaymentAmount'un tamamını tek ürün olarak gönderiyoruz.
        var userBasket = new object[][]
        {
            new object[] { product.Name, request.PaymentAmount.ToString("F2"), quantity}
        };

        var userBasketJson = JsonSerializer.Serialize(userBasket);

        var userBasketStr = Convert.ToBase64String(Encoding.UTF8.GetBytes(userBasketJson));

        // Sabit ayarlar
        string noInstallment = "0"; // Taksit istemiyorsanız 1 yapın
        string maxInstallment = "12";
        string currency = request.Currency.ToString(); // "TRY", "USD" veya "EUR"
        string testMode = _env.IsDevelopment() ? "1" : "0"; // Canlıda 0, test için 1
        string debugOn = _env.IsDevelopment() ? "1" : "0";  // Hata ayıklama için 1 yapabilirsiniz
        string timeoutLimit = "300";
        string lang = "tr";

        // Token oluşturma
        var concatStr = string.Concat(
            merchantId,
            userIp,
            merchantOid,
            email,
            paymentAmountKurus.ToString(),
            userBasketStr,
            noInstallment,
            maxInstallment,
            currency,
            testMode,
            merchantSalt
        );

        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(merchantKey));
        var tokenBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(concatStr));
        var paytrToken = Convert.ToBase64String(tokenBytes);

        var dataDict = new Dictionary<string, string>
        {
            ["merchant_id"] = merchantId,
            ["user_ip"] = userIp,
            ["merchant_oid"] = merchantOid,
            ["email"] = email,
            ["payment_amount"] = paymentAmountKurus.ToString(),
            ["user_basket"] = userBasketStr,
            ["paytr_token"] = paytrToken,
            ["debug_on"] = debugOn,
            ["test_mode"] = testMode,
            ["no_installment"] = noInstallment,
            ["max_installment"] = maxInstallment,
            ["user_name"] = fullName,
            ["user_address"] = address,
            ["user_phone"] = phoneNumber,
            ["merchant_ok_url"] = merchantOkUrl,
            ["merchant_fail_url"] = merchantFailUrl,
            ["timeout_limit"] = timeoutLimit,
            ["currency"] = currency,
            ["lang"] = lang
        };

        using var client = _httpClientFactory.CreateClient();

        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://www.paytr.com/odeme/api/get-token")
        {
            Content = new FormUrlEncodedContent(dataDict!)
        };

        var response = await client.SendAsync(requestMessage, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return StatusCode((int)response.StatusCode, "PAYTR isteği başarısız oldu.");
        }

        var responseString = await response.Content.ReadAsStringAsync(cancellationToken);

        using var doc = JsonDocument.Parse(responseString);

        if (doc.RootElement.TryGetProperty("status", out var statusElem) && statusElem.GetString() == "success")
        {
            var token = doc.RootElement.GetProperty("token").GetString();

            var iframeUrl = "https://www.paytr.com/odeme/guvenli/" + token;

            // İsterseniz bu URL'yi direkt front-end'e dönebilirsiniz.
            return Ok(new GetPayTRTokenResponse(iframeUrl));
        }
        else
        {
            var reason = doc.RootElement.TryGetProperty("reason", out var reasonElem) ? reasonElem.GetString() : "Bilinmeyen hata";
            return BadRequest($"PAYTR IFRAME başarısız oldu. Sebep: {reason}");
        }
    }

    private string GetIpAddress()
    {
        if (_env.IsDevelopment())
            return IpHelper.GetIpAddress();

        if (HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
            return HttpContext.Request.Headers["X-Forwarded-For"];
        else
            return HttpContext.Connection.RemoteIpAddress?.ToString();
    }
}
