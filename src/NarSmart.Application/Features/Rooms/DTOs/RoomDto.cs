namespace NarSmart.Application.Features.Rooms.DTOs;

public class RoomDto
{
    public Guid Id { get; set; }
    public string RoomNumber { get; set; } = null!;
    public int FloorNumber { get; set; }
    public int BedCount { get; set; }
    public bool IsActive { get; set; }
}
