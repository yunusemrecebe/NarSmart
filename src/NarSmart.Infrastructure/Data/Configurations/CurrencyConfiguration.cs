using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NarSmart.Domain.Entities.Currency;

namespace NarSmart.Infrastructure.Data.Configurations;

public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.ToTable("Currencies");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Code).HasMaxLength(3).IsRequired();
        builder.Property(c => c.Name).HasMaxLength(100).IsRequired();
        builder.Property(c => c.Symbol).HasMaxLength(10).IsRequired();
        builder.HasIndex(c => c.Code).IsUnique();
        builder.HasQueryFilter(c => !c.IsDeleted);
    }
}
