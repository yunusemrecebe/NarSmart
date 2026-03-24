namespace NarSmart.Domain.Entities.SalesPackage;

public interface ISalesPackageRepository
{
    Task<SalesPackage?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<SalesPackage>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(SalesPackage salesPackage, CancellationToken cancellationToken = default);
    void Update(SalesPackage salesPackage);
    void Delete(SalesPackage salesPackage);
}
