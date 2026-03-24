using NarSmart.Domain.Common;

namespace NarSmart.Domain.Entities.User;

public class UserHotel : BaseEntity
{
    public Guid UserId { get; private set; }
    public ApplicationUser User { get; private set; } = null!;
    public Entities.Hotel.Hotel Hotel { get; private set; } = null!;

    private UserHotel() { }

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
