using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NarSmart.Domain.Entities.Room;

namespace NarSmart.Infrastructure.Data.Configurations;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("Rooms");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.RoomNumber).HasMaxLength(20).IsRequired();
        builder.HasIndex(r => new { r.HotelId, r.RoomNumber }).IsUnique();
    }
}
