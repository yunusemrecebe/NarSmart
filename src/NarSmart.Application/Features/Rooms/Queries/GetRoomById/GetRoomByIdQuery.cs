using MediatR;
using NarSmart.Application.Common.Models;
using NarSmart.Application.Features.Rooms.DTOs;

namespace NarSmart.Application.Features.Rooms.Queries.GetRoomById;

public record GetRoomByIdQuery(Guid Id) : IRequest<Result<RoomDto>>;
