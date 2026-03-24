namespace NarSmart.Application.Common.Interfaces;

public interface ICurrentTenantService
{
    Guid HotelId { get; }
    Guid UserId { get; }
    string Role { get; }
}
