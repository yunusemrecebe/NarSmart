using MediatR;
using NarSmart.Application.Common.Interfaces;
using NarSmart.Application.Common.Models;

namespace NarSmart.Application.Features.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<bool>>
{
    private readonly IUserService _userService;
    private readonly ICurrentTenantService _tenantService;

    public DeleteUserCommandHandler(IUserService userService, ICurrentTenantService tenantService)
    {
        _userService = userService;
        _tenantService = tenantService;
    }

    public async Task<Result<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        return await _userService.DeleteAsync(
            request.Id, _tenantService.HotelId, request.TerminationDate, cancellationToken);
    }
}
