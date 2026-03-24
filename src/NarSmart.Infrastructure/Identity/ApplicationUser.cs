using Microsoft.AspNetCore.Identity;

namespace NarSmart.Infrastructure.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public Guid HotelId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string RegistrationNumber { get; set; } = null!;
    public string? PhotoUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; }
}
