using NarSmart.Domain.Common;

namespace NarSmart.Infrastructure.Identity;

public class UserHotel : BaseEntity
{
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;
    public Domain.Entities.Hotel.Hotel Hotel { get; set; } = null!;

    public static UserHotel Create(Guid userId, Guid hotelId)
    {
        return new UserHotel
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            HotelId = hotelId
        };
    }
}
