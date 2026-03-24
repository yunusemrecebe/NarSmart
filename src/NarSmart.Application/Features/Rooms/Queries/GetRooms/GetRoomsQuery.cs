using MediatR;
using NarSmart.Application.Common.Models;
using NarSmart.Application.Features.Rooms.DTOs;

namespace NarSmart.Application.Features.Rooms.Queries.GetRooms;

public record GetRoomsQuery : IRequest<Result<List<RoomDto>>>;
