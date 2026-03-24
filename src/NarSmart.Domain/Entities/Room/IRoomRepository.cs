namespace NarSmart.Domain.Entities.Room;

public interface IRoomRepository
{
    Task<Room?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Room>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<bool> ExistsByRoomNumberAsync(Guid hotelId, string roomNumber, CancellationToken cancellationToken = default);
    Task AddAsync(Room room, CancellationToken cancellationToken = default);
    void Update(Room room);
    void Delete(Room room);
}
