using NarSmart.Domain.Enums;

namespace NarSmart.Application.Features.Customers.DTOs;

public class CustomerDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? NationalId { get; set; }
    public string? PassportNumber { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public DateTime BirthDate { get; set; }
    public bool IsAdult { get; set; }
    public string? ProfilePhotoUrl { get; set; }
    public CustomerType CustomerType { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsActive { get; set; }
}
