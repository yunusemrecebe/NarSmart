using MediatR;
using NarSmart.Application.Common.Models;

namespace NarSmart.Application.Features.Rooms.Commands.CreateRoom;

public record CreateRoomCommand(
    string RoomNumber,
    int FloorNumber,
    int BedCount) : IRequest<Result<Guid>>;
