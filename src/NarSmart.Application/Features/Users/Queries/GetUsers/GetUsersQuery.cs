using MediatR;
using NarSmart.Application.Common.Models;
using NarSmart.Application.Features.Users.DTOs;

namespace NarSmart.Application.Features.Users.Queries.GetUsers;

public record GetUsersQuery : IRequest<Result<List<UserDto>>>;
