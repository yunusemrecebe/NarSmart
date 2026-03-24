using MediatR;
using NarSmart.Application.Common.Interfaces;
using NarSmart.Application.Common.Models;
using NarSmart.Application.Features.Users.DTOs;

namespace NarSmart.Application.Features.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
{
    private readonly IUserService _userService;
    private readonly ICurrentTenantService _tenantService;

    public GetUserByIdQueryHandler(IUserService userService, ICurrentTenantService tenantService)
    {
        _userService = userService;
        _tenantService = tenantService;
    }

    public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        return await _userService.GetByIdAsync(request.Id, _tenantService.HotelId, cancellationToken);
    }
}
