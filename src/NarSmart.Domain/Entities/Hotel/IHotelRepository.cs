namespace NarSmart.Domain.Entities.Hotel;

public interface IHotelRepository
{
    Task<Hotel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Hotel hotel, CancellationToken cancellationToken = default);
    void Update(Hotel hotel);
}
