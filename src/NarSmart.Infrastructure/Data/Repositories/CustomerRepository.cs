using Microsoft.EntityFrameworkCore;
using NarSmart.Domain.Entities.Customer;

namespace NarSmart.Infrastructure.Data.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly NarSmartDbContext _context;

    public CustomerRepository(NarSmartDbContext context) => _context = context;

    public async Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _context.Customers.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

    public async Task<List<Customer>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _context.Customers.ToListAsync(cancellationToken);

    public async Task AddAsync(Customer customer, CancellationToken cancellationToken = default)
        => await _context.Customers.AddAsync(customer, cancellationToken);

    public void Update(Customer customer) => _context.Customers.Update(customer);

    public void Delete(Customer customer) => _context.Customers.Remove(customer);
}
