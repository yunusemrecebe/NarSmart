using MediatR;
using NarSmart.Application.Common.Models;
using NarSmart.Application.Features.Users.DTOs;

namespace NarSmart.Application.Features.Users.Queries.GetUserById;

public record GetUserByIdQuery(Guid Id) : IRequest<Result<UserDto>>;
