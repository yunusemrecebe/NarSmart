using MediatR;
using NarSmart.Application.Common.Models;
using NarSmart.Application.Features.Rooms.DTOs;
using NarSmart.Domain.Entities.Room;

namespace NarSmart.Application.Features.Rooms.Queries.GetRooms;

public class GetRoomsQueryHandler : IRequestHandler<GetRoomsQuery, Result<List<RoomDto>>>
{
    private readonly IRoomRepository _roomRepository;

    public GetRoomsQueryHandler(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<Result<List<RoomDto>>> Handle(GetRoomsQuery request, CancellationToken cancellationToken)
    {
        var rooms = await _roomRepository.GetAllAsync(cancellationToken);
        return Result<List<RoomDto>>.Success(rooms.ToDtoList());
    }
}
