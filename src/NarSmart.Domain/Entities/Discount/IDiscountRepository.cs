namespace NarSmart.Domain.Entities.Discount;

public interface IDiscountRepository
{
    Task<Discount?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Discount>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Discount discount, CancellationToken cancellationToken = default);
    void Update(Discount discount);
    void Delete(Discount discount);
}
