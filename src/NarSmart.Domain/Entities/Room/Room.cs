using NarSmart.Domain.Common;

namespace NarSmart.Domain.Entities.Room;

public class Room : BaseEntity
{
    public string RoomNumber { get; private set; } = null!;
    public int FloorNumber { get; private set; }
    public int BedCount { get; private set; }

    private Room() { }

    public static Room Create(Guid hotelId, string roomNumber, int floorNumber, int bedCount)
    {
        if (string.IsNullOrWhiteSpace(roomNumber))
            throw new DomainException("Room number cannot be empty.");

        if (bedCount <= 0)
            throw new DomainException("Bed count must be greater than zero.");

        return new Room
        {
            Id = Guid.NewGuid(),
            HotelId = hotelId,
            RoomNumber = roomNumber,
            FloorNumber = floorNumber,
            BedCount = bedCount
        };
    }

    public void Update(string roomNumber, int floorNumber, int bedCount)
    {
        if (string.IsNullOrWhiteSpace(roomNumber))
            throw new DomainException("Room number cannot be empty.");

        if (bedCount <= 0)
            throw new DomainException("Bed count must be greater than zero.");

        RoomNumber = roomNumber;
        FloorNumber = floorNumber;
        BedCount = bedCount;
    }
}
