using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using YazilimAcademyPayments.WebApi.Persistence.EntityFramework.Contexts;

public sealed class ApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly ApplicationDbContext _context;
    private readonly IDistributedCache _cache;

    public ApiKeyAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        ApplicationDbContext context,
        IDistributedCache cache)
        : base(options, logger, encoder, clock)
    {
        _context = context;
        _cache = cache;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue("X-Api-Key", out var apiKeyHeaderValues))
        {
            return AuthenticateResult.NoResult();
        }

        var apiKey = apiKeyHeaderValues.ToString();

        // Önce önbellekten TenantId'yi çekmeyi deneyin
        var cachedTenantId = await _cache.GetStringAsync("ApiKey_" + apiKey);

        Guid tenantId;

        if (!string.IsNullOrEmpty(cachedTenantId) && Guid.TryParse(cachedTenantId, out tenantId))
        {

        }
        else
        {
            // Veritabanından kontrol et
            var tenantApiKey = await _context.TenantApiKeys
                .Where(x => x.ApiKey == apiKey && x.IsActive)
                .FirstOrDefaultAsync();

            if (tenantApiKey == null)
                return AuthenticateResult.Fail("Geçersiz API Anahtarı");

            tenantId = tenantApiKey.TenantId;

            // Önbelleğe ekleyin (örneğin 5 dakika süre ile)
            await _cache.SetStringAsync("ApiKey_" + apiKey, tenantId.ToString(), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60),
                SlidingExpiration = TimeSpan.FromMinutes(30)
            });
        }

        // Kimliği oluştur
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, tenantId.ToString()),
            new Claim("TenantId", tenantId.ToString()),
        };

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
}
