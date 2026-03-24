using MediatR;
using NarSmart.Application.Common.Interfaces;
using NarSmart.Application.Common.Models;
using NarSmart.Application.Features.Users.DTOs;

namespace NarSmart.Application.Features.Users.Queries.GetUsers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, Result<List<UserDto>>>
{
    private readonly IUserService _userService;
    private readonly ICurrentTenantService _tenantService;

    public GetUsersQueryHandler(IUserService userService, ICurrentTenantService tenantService)
    {
        _userService = userService;
        _tenantService = tenantService;
    }

    public async Task<Result<List<UserDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await _userService.GetAllByHotelAsync(_tenantService.HotelId, cancellationToken);
    }
}
