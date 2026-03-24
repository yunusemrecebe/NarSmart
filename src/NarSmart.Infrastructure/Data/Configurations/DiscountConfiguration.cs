using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NarSmart.Domain.Entities.Discount;

namespace NarSmart.Infrastructure.Data.Configurations;

public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        builder.ToTable("Discounts");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Amount).HasPrecision(18, 2);
    }
}
