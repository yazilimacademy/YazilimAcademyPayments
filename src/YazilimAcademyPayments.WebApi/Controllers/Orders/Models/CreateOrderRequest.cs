
using System.ComponentModel.DataAnnotations;
using YazilimAcademyPayments.WebApi.Domain.Entities;
using YazilimAcademyPayments.WebApi.Domain.Enums;

namespace YazilimAcademyPayments.WebApi.Controllers.Orders.Models;

public sealed record CreateOrderRequest
{
    [Required(ErrorMessage = "ProductId alanı zorunludur.")]
    public Guid ProductId { get; set; }

    [Required(ErrorMessage = "MerchantOid alanı zorunludur.")]
    [MaxLength(100, ErrorMessage = "MerchantOid alanı en fazla 100 karakter olmalıdır.")]
    [MinLength(6, ErrorMessage = "MerchantOid alanı en az 6 karakter olmalıdır.")]
    public string MerchantOid { get; set; } = null!;

    [Required(ErrorMessage = "PaymentAmount alanı zorunludur.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "PaymentAmount alanı 0.01'den büyük olmalıdır.")]
    public decimal PaymentAmount { get; set; }

    [Required(ErrorMessage = "Currency alanı zorunludur.")]
    [EnumDataType(typeof(CurrencyCode), ErrorMessage = "Geçersiz CurrencyCode.")]
    [AllowedValues(CurrencyCode.TRY, CurrencyCode.USD, CurrencyCode.EUR, ErrorMessage = "Geçersiz Para Birimi.")]
    public CurrencyCode Currency { get; set; }

    public Guid? PromotionId { get; set; }


    public CreateOrderRequestUserDto User { get; set; } = null!;

    public CreateOrderRequest(Guid productId, string merchantOid, decimal paymentAmount, CurrencyCode currency, Guid? promotionId, CreateOrderRequestUserDto user)
    {
        ProductId = productId;
        MerchantOid = merchantOid;
        PaymentAmount = paymentAmount;
        Currency = currency;
        PromotionId = promotionId;
        User = user;
    }

    public CreateOrderRequest()
    {

    }

    public static Order ToOrder(CreateOrderRequest request, Guid tenantId, Guid userId)
    {

        var orderId = Guid.CreateVersion7();

        return new Order
        {
            Id = orderId,
            TenantId = tenantId,
            UserId = userId,
            ProductId = request.ProductId,
            MerchantOid = request.MerchantOid,
            PaymentAmount = request.PaymentAmount,
            Currency = request.Currency,
            PromotionId = request.PromotionId,
            CreatedAt = DateTimeOffset.UtcNow,
            Histories = new List<OrderHistory>
            {
                new OrderHistory
                {
                    Id = Guid.CreateVersion7(),
                    OrderId = orderId,
                    Status = OrderStatus.Pending,
                    CreatedAt = DateTimeOffset.UtcNow,
                }
            }
        };
    }
}
