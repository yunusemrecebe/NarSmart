using NarSmart.Domain.Common;

namespace NarSmart.Domain.Entities.HotelService;

public class HotelServiceImage : BaseEntity
{
    public Guid HotelServiceId { get; private set; }
    public string ImageUrl { get; private set; } = null!;
    public int DisplayOrder { get; private set; }

    private HotelServiceImage() { }

    internal static HotelServiceImage Create(Guid hotelServiceId, Guid hotelId, string imageUrl, int displayOrder)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
            throw new DomainException("Image URL cannot be empty.");

        return new HotelServiceImage
        {
            Id = Guid.NewGuid(),
            HotelId = hotelId,
            HotelServiceId = hotelServiceId,
            ImageUrl = imageUrl,
            DisplayOrder = displayOrder
        };
    }
}
