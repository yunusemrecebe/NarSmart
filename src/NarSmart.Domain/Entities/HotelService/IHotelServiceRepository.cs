namespace NarSmart.Domain.Entities.HotelService;

public interface IHotelServiceRepository
{
    Task<HotelService?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<HotelService>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(HotelService hotelService, CancellationToken cancellationToken = default);
    void Update(HotelService hotelService);
    void Delete(HotelService hotelService);
}
