using Microsoft.EntityFrameworkCore;
using NarSmart.Domain.Entities.Room;

namespace NarSmart.Infrastructure.Data.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly NarSmartDbContext _context;

    public RoomRepository(NarSmartDbContext context) => _context = context;

    public async Task<Room?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _context.Rooms.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

    public async Task<List<Room>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _context.Rooms.ToListAsync(cancellationToken);

    public async Task<bool> ExistsByRoomNumberAsync(Guid hotelId, string roomNumber, CancellationToken cancellationToken = default)
        => await _context.Rooms.AnyAsync(r => r.RoomNumber == roomNumber, cancellationToken);

    public async Task AddAsync(Room room, CancellationToken cancellationToken = default)
        => await _context.Rooms.AddAsync(room, cancellationToken);

    public void Update(Room room) => _context.Rooms.Update(room);

    public void Delete(Room room) => _context.Rooms.Remove(room);
}
