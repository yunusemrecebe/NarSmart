using MediatR;
using NarSmart.Application.Common.Interfaces;
using NarSmart.Application.Common.Models;

namespace NarSmart.Application.Features.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<bool>>
{
    private readonly IUserService _userService;
    private readonly ICurrentTenantService _tenantService;

    public UpdateUserCommandHandler(IUserService userService, ICurrentTenantService tenantService)
    {
        _userService = userService;
        _tenantService = tenantService;
    }

    public async Task<Result<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        return await _userService.UpdateAsync(
            request.Id, _tenantService.HotelId,
            request.FirstName, request.LastName, request.RegistrationNumber,
            request.PhoneNumber, request.PhotoUrl, request.HireDate,
            cancellationToken);
    }
}
