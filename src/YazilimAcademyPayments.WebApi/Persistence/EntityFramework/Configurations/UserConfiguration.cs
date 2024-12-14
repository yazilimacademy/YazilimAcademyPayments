using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YazilimAcademyPayments.WebApi.Domain.Entities;
using YazilimAcademyPayments.WebApi.Domain.ValueObjects;

namespace YazilimAcademyPayments.WebApi.Persistence.EntityFramework.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(u => new { u.TenantId, u.Email }).IsUnique();

        builder.Property(u => u.Email)
        .HasConversion(x => x.Value, x => new Email(x));

        builder.Property(u => u.PhoneNumber)
        .HasConversion(x => x.Value, x => new PhoneNumber(x));
    }

}
