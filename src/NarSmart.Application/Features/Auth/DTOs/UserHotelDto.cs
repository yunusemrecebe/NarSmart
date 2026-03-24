namespace NarSmart.Application.Features.Auth.DTOs;

public class UserHotelDto
{
    public Guid HotelId { get; set; }
    public string HotelName { get; set; } = null!;
    public string Location { get; set; } = null!;
}
