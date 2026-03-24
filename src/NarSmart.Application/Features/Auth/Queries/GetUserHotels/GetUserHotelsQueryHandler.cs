using MediatR;
using NarSmart.Application.Common.Interfaces;
using NarSmart.Application.Common.Models;
using NarSmart.Application.Features.Auth.DTOs;

namespace NarSmart.Application.Features.Auth.Queries.GetUserHotels;

public class GetUserHotelsQueryHandler : IRequestHandler<GetUserHotelsQuery, Result<List<UserHotelDto>>>
{
    private readonly IAuthService _authService;

    public GetUserHotelsQueryHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Result<List<UserHotelDto>>> Handle(GetUserHotelsQuery request, CancellationToken cancellationToken)
    {
        return await _authService.GetUserHotelsAsync(request.Email, cancellationToken);
    }
}
