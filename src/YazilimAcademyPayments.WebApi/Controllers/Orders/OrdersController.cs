using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YazilimAcademyPayments.WebApi.Controllers.Orders.Models;
using YazilimAcademyPayments.WebApi.Extensions;
using YazilimAcademyPayments.WebApi.Persistence.EntityFramework.Contexts;

namespace YazilimAcademyPayments.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "ApiKeyScheme")]
    public sealed class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public OrdersController(ApplicationDbContext context, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
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
        public async Task<IActionResult> GetPayTRTokenAsync(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            // Bu değerleri konfigürasyondan okuyun
            var merchant_id = _configuration["PayTR:MerchantId"];
            var merchant_key = _configuration["PayTR:MerchantKey"];
            var merchant_salt = _configuration["PayTR:MerchantSalt"];

            if (string.IsNullOrEmpty(merchant_id) || string.IsNullOrEmpty(merchant_key) || string.IsNullOrEmpty(merchant_salt))
                return StatusCode(500, "PayTR yapılandırması eksik.");

            var tenantId = HttpContext.User.GetTenantId();

            var user = await _context
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.TenantId == tenantId && u.Email == request.User.Email, cancellationToken);

            if (user is null)
            {
                user = CreateOrderRequestUserDto.From(request.User, tenantId);
                _context.Users.Add(user);
                await _context.SaveChangesAsync(cancellationToken);
            }

            // PayTR'ın istediği formatta tutar: Örn. 9.99 için 999 gönderilmeli.
            int payment_amount_kurus = (int)(request.PaymentAmount * 100);

            // Sipariş numarası (merchant_oid): request.MerchantOid
            var merchant_oid = request.MerchantOid;

            // Kullanıcı bilgileri
            var emailstr = (string)user.Email;
            var user_namestr = user.FullName.ToString();
            var user_addressstr = request.User.Address;   // Address bilgisinin User tablonuzda tutulduğunu varsayalım
            var user_phonestr = (string)user.PhoneNumber;

            // Başarılı/başarısız dönüş URL'leri
            var merchant_ok_url = _configuration["PayTR:OkUrl"] ?? "https://www.siteniz.com/basarili";
            var merchant_fail_url = _configuration["PayTR:FailUrl"] ?? "https://www.siteniz.com/basarisiz";

            // Kullanıcı IP alma
            var user_ip = Request.Headers["X-Forwarded-For"].FirstOrDefault()
                          ?? HttpContext.Connection.RemoteIpAddress?.ToString()
                          ?? "127.0.0.1";

            // Sepet içeriği (Örnek tek ürün)
            // Burada request'ten gelen product bilgilerini kullanabilir, birim fiyat ve adet belirtebilirsiniz.
            // PayTR her ürün için ad, fiyat ve adet bekliyor.
            // Bu örnekte PaymentAmount'un tamamını tek ürün olarak gönderiyoruz.
            var user_basket = new object[][]
            {
                new object[] { "Ürün", request.PaymentAmount.ToString("F2"), 1 }
            };

            var user_basket_json = JsonSerializer.Serialize(user_basket);
            var user_basketstr = Convert.ToBase64String(Encoding.UTF8.GetBytes(user_basket_json));

            // Sabit ayarlar
            string no_installment = "0"; // Taksit istemiyorsanız 1 yapın
            string max_installment = "0";
            string currency = request.Currency.ToString(); // "TRY", "USD" veya "EUR"
            string test_mode = "0"; // Canlıda 0, test için 1
            string debug_on = "0";  // Hata ayıklama için 1 yapabilirsiniz
            string timeout_limit = "30";
            string lang = "tr";

            // Token oluşturma
            var concatStr = string.Concat(
                merchant_id,
                user_ip,
                merchant_oid,
                emailstr,
                payment_amount_kurus.ToString(),
                user_basketstr,
                no_installment,
                max_installment,
                currency,
                test_mode,
                merchant_salt
            );

            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(merchant_key));
            var tokenBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(concatStr));
            var paytr_token = Convert.ToBase64String(tokenBytes);

            var dataDict = new Dictionary<string, string>
            {
                ["merchant_id"] = merchant_id,
                ["user_ip"] = user_ip,
                ["merchant_oid"] = merchant_oid,
                ["email"] = emailstr,
                ["payment_amount"] = payment_amount_kurus.ToString(),
                ["user_basket"] = user_basketstr,
                ["paytr_token"] = paytr_token,
                ["debug_on"] = debug_on,
                ["test_mode"] = test_mode,
                ["no_installment"] = no_installment,
                ["max_installment"] = max_installment,
                ["user_name"] = user_namestr,
                ["user_address"] = user_addressstr,
                ["user_phone"] = user_phonestr,
                ["merchant_ok_url"] = merchant_ok_url,
                ["merchant_fail_url"] = merchant_fail_url,
                ["timeout_limit"] = timeout_limit,
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
                return Ok(new { IframeUrl = iframeUrl });
            }
            else
            {
                var reason = doc.RootElement.TryGetProperty("reason", out var reasonElem) ? reasonElem.GetString() : "Bilinmeyen hata";
                return BadRequest($"PAYTR IFRAME başarısız oldu. Sebep: {reason}");
            }
        }
    }
}
