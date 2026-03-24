namespace NarSmart.Domain.Entities.User;

public class ApplicationUser
{
    public Guid Id { get; set; }
    public Guid HotelId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string RegistrationNumber { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? PhotoUrl { get; set; }
    public string Email { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; }

    public ICollection<UserHotel> UserHotels { get; set; } = new List<UserHotel>();
}
