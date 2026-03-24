namespace NarSmart.Domain.Entities.Sale;

public interface ISaleRepository
{
    Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Sale>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<bool> HasOverlappingReservationAsync(Guid hotelId, Guid roomId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    Task AddAsync(Sale sale, CancellationToken cancellationToken = default);
    void Update(Sale sale);
}
