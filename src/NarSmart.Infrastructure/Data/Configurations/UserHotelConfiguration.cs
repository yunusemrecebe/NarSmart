using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NarSmart.Infrastructure.Identity;

namespace NarSmart.Infrastructure.Data.Configurations;

public class UserHotelConfiguration : IEntityTypeConfiguration<UserHotel>
{
    public void Configure(EntityTypeBuilder<UserHotel> builder)
    {
        builder.ToTable("UserHotels");
        builder.HasKey(uh => uh.Id);
        builder.HasIndex(uh => new { uh.UserId, uh.HotelId }).IsUnique();

        builder.HasOne(uh => uh.User)
            .WithMany()
            .HasForeignKey(uh => uh.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(uh => uh.Hotel)
            .WithMany()
            .HasForeignKey(uh => uh.HotelId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
