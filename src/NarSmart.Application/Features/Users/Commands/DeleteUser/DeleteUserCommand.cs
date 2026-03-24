using MediatR;
using NarSmart.Application.Common.Models;

namespace NarSmart.Application.Features.Users.Commands.DeleteUser;

public record DeleteUserCommand(Guid Id, DateTime TerminationDate) : IRequest<Result<bool>>;
