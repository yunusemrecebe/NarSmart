using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using NarSmart.Application.Common.Interfaces;

namespace NarSmart.Infrastructure.Services;

public class CurrentTenantService : ICurrentTenantService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentTenantService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid HotelId
    {
        get
        {
            var claim = _httpContextAccessor.HttpContext?.User.FindFirst("hotelId")?.Value;
            return claim is not null ? Guid.Parse(claim) : Guid.Empty;
        }
    }

    public Guid UserId
    {
        get
        {
            var claim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return claim is not null ? Guid.Parse(claim) : Guid.Empty;
        }
    }

    public string Role
    {
        get
        {
            return _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;
        }
    }
}
