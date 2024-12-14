using YazilimAcademyPayments.WebApi.Domain.Common;

namespace YazilimAcademyPayments.WebApi.Domain.Entities;

public sealed class TenantApiKey : EntityBase
{
    public Guid TenantId { get; set; }
    public Tenant Tenant { get; set; }

    public string ApiKey { get; set; }
    public bool IsActive { get; set; }

}
