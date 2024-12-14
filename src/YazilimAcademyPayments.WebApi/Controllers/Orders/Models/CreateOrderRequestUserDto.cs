using System.ComponentModel.DataAnnotations;
using YazilimAcademyPayments.WebApi.Domain.Entities;
using YazilimAcademyPayments.WebApi.Domain.ValueObjects;

namespace YazilimAcademyPayments.WebApi.Controllers.Orders.Models;

public sealed record CreateOrderRequestUserDto
{
    [Required(ErrorMessage = "FullName alanı zorunludur.")]
    [MaxLength(100, ErrorMessage = "FullName alanı en fazla 100 karakter olmalıdır.")]
    public string Name { get; set; }


    [Required(ErrorMessage = "Email alanı zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçersiz Email Adresi.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "PhoneNumber alanı zorunludur.")]
    [Phone(ErrorMessage = "Geçersiz Telefon Numarası.")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Adres alanı zorunludur.")]
    [MaxLength(500, ErrorMessage = "Adres alanı en fazla 500 karakter olmalıdır.")]
    [MinLength(10, ErrorMessage = "Adres alanı en az 10 karakter olmalıdır.")]
    public string Address { get; set; }

    public string? ExternalUserId { get; set; }

    public CreateOrderRequestUserDto(string name, string email, string phoneNumber, string address, string? externalUserId = null)
    {
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        Address = address;
        ExternalUserId = externalUserId;
    }

    public CreateOrderRequestUserDto()
    {

    }

    public static User From(CreateOrderRequestUserDto dto, Guid tenantId)
    {
        return new User
        {
            Id = Guid.CreateVersion7(),
            FullName = FullName.Create(dto.Name),
            Email = new Email(dto.Email),
            PhoneNumber = new PhoneNumber(dto.PhoneNumber),
            TenantId = tenantId,
            ExternalUserId = dto.ExternalUserId,
            CreatedAt = DateTimeOffset.UtcNow,
        };
    }
}
