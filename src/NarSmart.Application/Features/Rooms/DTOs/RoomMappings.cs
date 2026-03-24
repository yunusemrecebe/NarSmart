using NarSmart.Domain.Entities.Room;

namespace NarSmart.Application.Features.Rooms.DTOs;

public static class RoomMappings
{
    public static RoomDto ToDto(this Room room)
    {
        return new RoomDto
        {
            Id = room.Id,
            RoomNumber = room.RoomNumber,
            FloorNumber = room.FloorNumber,
            BedCount = room.BedCount,
            IsActive = room.IsActive
        };
    }

    public static List<RoomDto> ToDtoList(this IEnumerable<Room> rooms)
        => rooms.Select(r => r.ToDto()).ToList();
}
