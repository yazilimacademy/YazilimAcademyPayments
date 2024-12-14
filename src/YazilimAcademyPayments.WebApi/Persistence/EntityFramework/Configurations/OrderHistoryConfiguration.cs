using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YazilimAcademyPayments.WebApi.Domain.Entities;
using YazilimAcademyPayments.WebApi.Domain.Enums;

namespace YazilimAcademyPayments.WebApi.Persistence.EntityFramework.Configurations;

public sealed class OrderHistoryConfiguration : IEntityTypeConfiguration<OrderHistory>
{
    public void Configure(EntityTypeBuilder<OrderHistory> builder)
    {
        // ID Primary Key
        builder.HasKey(oh => oh.Id);

        // OrderStatus Enum
        builder.Property(oh => oh.Status)
            .HasConversion<int>()
            .IsRequired()
            .HasDefaultValue(OrderStatus.Pending)
            .HasColumnType("tinyint");


        // Description
        builder.Property(oh => oh.Description)
            .HasMaxLength(2500)
            .IsRequired(false);

        // Common Properties
        builder.Property(e => e.CreatedAt)
            .IsRequired()
            .HasDefaultValue(DateTimeOffset.UtcNow);

        builder.Property(e => e.UpdatedAt)
            .IsRequired(false);

        // //Relationships
        // builder.HasOne(oh => oh.Order)
        //     .WithMany(o => o.Histories)
        //     .HasForeignKey(oh => oh.OrderId);

        builder.ToTable("order_histories");
    }
}
