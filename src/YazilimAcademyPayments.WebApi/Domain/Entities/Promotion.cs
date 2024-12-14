using YazilimAcademyPayments.WebApi.Domain.Common;
using YazilimAcademyPayments.WebApi.Domain.Enums;

namespace YazilimAcademyPayments.WebApi.Domain.Entities;

public sealed class Promotion : EntityBase
{
    public Guid ProductId { get; set; } // Pro Gold
    public Product Product { get; set; }
    public string Code { get; set; } // YAZ2024ProGold
    public string Name { get; set; } // 2024 YAZ Ogrenci Indirimi Pro Gold
    public string? Description { get; set; } // Gencler sevinsin iste Alper Hocam.
    public decimal DiscountAmount { get; set; } // 50
    public DateTimeOffset StartDate { get; set; } // 2024-01-01
    public DateTimeOffset EndDate { get; set; } // 2024-01-31
    public PromotionType Type { get; set; } // Percentage
    public bool IsActive { get; set; } // true
    public int MaxUsageCount { get; set; } // 100
    public int UsageCount { get; set; } // 0

    // Concurrency
    public byte[] RowVersion { get; set; }
}
