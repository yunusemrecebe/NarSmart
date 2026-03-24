using MediatR;
using NarSmart.Application.Common.Interfaces;
using NarSmart.Application.Common.Models;
using NarSmart.Domain.Common;
using NarSmart.Domain.Entities.Room;

namespace NarSmart.Application.Features.Rooms.Commands.CreateRoom;

public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, Result<Guid>>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentTenantService _tenantService;

    public CreateRoomCommandHandler(
        IRoomRepository roomRepository,
        IUnitOfWork unitOfWork,
        ICurrentTenantService tenantService)
    {
        _roomRepository = roomRepository;
        _unitOfWork = unitOfWork;
        _tenantService = tenantService;
    }

    public async Task<Result<Guid>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        var exists = await _roomRepository.ExistsByRoomNumberAsync(
            _tenantService.HotelId, request.RoomNumber, cancellationToken);

        if (exists)
            return Result<Guid>.Failure($"Room number '{request.RoomNumber}' already exists.");

        var room = Room.Create(_tenantService.HotelId, request.RoomNumber, request.FloorNumber, request.BedCount);

        await _roomRepository.AddAsync(room, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Created(room.Id);
    }
}
