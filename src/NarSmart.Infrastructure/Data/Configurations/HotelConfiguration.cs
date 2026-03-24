using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NarSmart.Domain.Entities.Hotel;

namespace NarSmart.Infrastructure.Data.Configurations;

public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        builder.ToTable("Hotels");
        builder.HasKey(h => h.Id);
        builder.Property(h => h.Name).HasMaxLength(200).IsRequired();
        builder.Property(h => h.Location).HasMaxLength(500).IsRequired();
        builder.Property(h => h.TimeZoneId).HasMaxLength(100).IsRequired();
    }
}
