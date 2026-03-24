using MediatR;
using NarSmart.Application.Common.Models;
using NarSmart.Application.Features.Rooms.DTOs;
using NarSmart.Domain.Entities.Room;

namespace NarSmart.Application.Features.Rooms.Queries.GetRoomById;

public class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery, Result<RoomDto>>
{
    private readonly IRoomRepository _roomRepository;

    public GetRoomByIdQueryHandler(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<Result<RoomDto>> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
    {
        var room = await _roomRepository.GetByIdAsync(request.Id, cancellationToken);

        if (room is null)
            return Result<RoomDto>.NotFound($"Room with id '{request.Id}' was not found.");

        return Result<RoomDto>.Success(room.ToDto());
    }
}
