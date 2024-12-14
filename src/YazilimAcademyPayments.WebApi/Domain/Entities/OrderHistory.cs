using YazilimAcademyPayments.WebApi.Domain.Common;
using YazilimAcademyPayments.WebApi.Domain.Enums;

namespace YazilimAcademyPayments.WebApi.Domain.Entities;


public sealed class OrderHistory : EntityBase
{
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
    public OrderStatus Status { get; set; }
    public string? Description { get; set; }
}
