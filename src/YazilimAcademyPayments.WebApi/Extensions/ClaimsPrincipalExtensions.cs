using System;
using System.Security.Claims;

namespace YazilimAcademyPayments.WebApi.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetTenantId(this ClaimsPrincipal user)
    {
        // Burada "TenantId" claim'i ile eşleşen bir claim'i buluyoruz.
        // Büyük-küçük harf duyarlılığı genelde yoktur, ama tutarlılık açısından
        // Authentication handler'da "TenantId" olarak set ettik.
        var claim = user.Claims.FirstOrDefault(c => c.Type == "TenantId");
        if (claim == null)
            throw new InvalidOperationException("TenantId claim yok.");

        if (!Guid.TryParse(claim.Value, out var tenantId))
            throw new InvalidOperationException("TenantId claim geçersiz.");

        return tenantId;
    }
}
