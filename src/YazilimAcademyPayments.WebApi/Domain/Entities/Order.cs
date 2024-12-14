using System;
using YazilimAcademyPayments.WebApi.Domain.Common;
using YazilimAcademyPayments.WebApi.Domain.Enums;

namespace YazilimAcademyPayments.WebApi.Domain.Entities;

public sealed class Order : EntityBase
{
    public Guid TenantId { get; set; }
    public Tenant Tenant { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public Guid? PromotionId { get; set; }
    public Promotion Promotion { get; set; }
    public string MerchantOid { get; set; } = null!;
    public decimal PaymentAmount { get; set; }
    public CurrencyCode Currency { get; set; }
    public OrderStatus Status { get; set; }
    public ICollection<Payment> Payments { get; set; } = [];
    public ICollection<OrderHistory> Histories { get; set; } = [];
}