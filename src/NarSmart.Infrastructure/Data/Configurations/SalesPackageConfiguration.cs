using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NarSmart.Domain.Entities.SalesPackage;

namespace NarSmart.Infrastructure.Data.Configurations;

public class SalesPackageConfiguration : IEntityTypeConfiguration<SalesPackage>
{
    public void Configure(EntityTypeBuilder<SalesPackage> builder)
    {
        builder.ToTable("SalesPackages");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Name).HasMaxLength(200).IsRequired();
        builder.Property(s => s.Description).HasMaxLength(2000);
        builder.Property(s => s.ImageUrl).HasMaxLength(1000);

        builder.HasMany(s => s.Prices)
            .WithOne()
            .HasForeignKey(p => p.SalesPackageId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.HotelServices)
            .WithOne()
            .HasForeignKey(hs => hs.SalesPackageId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class SalesPackagePriceConfiguration : IEntityTypeConfiguration<SalesPackagePrice>
{
    public void Configure(EntityTypeBuilder<SalesPackagePrice> builder)
    {
        builder.ToTable("SalesPackagePrices");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Amount).HasPrecision(18, 2);
        builder.HasIndex(p => new { p.SalesPackageId, p.CurrencyId }).IsUnique();
    }
}

public class SalesPackageHotelServiceConfiguration : IEntityTypeConfiguration<SalesPackageHotelService>
{
    public void Configure(EntityTypeBuilder<SalesPackageHotelService> builder)
    {
        builder.ToTable("SalesPackageHotelServices");
        builder.HasKey(s => s.Id);
        builder.HasIndex(s => new { s.SalesPackageId, s.HotelServiceId }).IsUnique();
    }
}
