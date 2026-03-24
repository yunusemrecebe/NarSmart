using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NarSmart.Domain.Entities.HotelService;

namespace NarSmart.Infrastructure.Data.Configurations;

public class HotelServiceConfiguration : IEntityTypeConfiguration<HotelService>
{
    public void Configure(EntityTypeBuilder<HotelService> builder)
    {
        builder.ToTable("HotelServices");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Name).HasMaxLength(200).IsRequired();
        builder.Property(s => s.Description).HasMaxLength(2000);

        builder.HasMany(s => s.Prices)
            .WithOne()
            .HasForeignKey(p => p.HotelServiceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.Images)
            .WithOne()
            .HasForeignKey(i => i.HotelServiceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class HotelServicePriceConfiguration : IEntityTypeConfiguration<HotelServicePrice>
{
    public void Configure(EntityTypeBuilder<HotelServicePrice> builder)
    {
        builder.ToTable("HotelServicePrices");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Amount).HasPrecision(18, 2);
        builder.HasIndex(p => new { p.HotelServiceId, p.CurrencyId }).IsUnique();
    }
}

public class HotelServiceImageConfiguration : IEntityTypeConfiguration<HotelServiceImage>
{
    public void Configure(EntityTypeBuilder<HotelServiceImage> builder)
    {
        builder.ToTable("HotelServiceImages");
        builder.HasKey(i => i.Id);
        builder.Property(i => i.ImageUrl).HasMaxLength(1000).IsRequired();
    }
}
