using System;
using YazilimAcademyPayments.WebApi.Domain.Common;

namespace YazilimAcademyPayments.WebApi.Domain.Entities;

public sealed class Product : EntityBase
{
    public Guid TenantId { get; set; }
    public Tenant Tenant { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public bool IsActive { get; set; }

    public ICollection<Promotion> Promotions { get; set; } = [];

}