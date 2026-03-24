using MediatR;
using NarSmart.Application.Common.Interfaces;
using NarSmart.Application.Common.Models;

namespace NarSmart.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<Guid>>
{
    private readonly IUserService _userService;
    private readonly ICurrentTenantService _tenantService;

    public CreateUserCommandHandler(IUserService userService, ICurrentTenantService tenantService)
    {
        _userService = userService;
        _tenantService = tenantService;
    }

    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        return await _userService.CreateAsync(
            _tenantService.HotelId,
            request.FirstName, request.LastName, request.Email,
            request.Password, request.RegistrationNumber, request.Role,
            request.PhoneNumber, request.PhotoUrl, request.HireDate,
            cancellationToken);
    }
}
