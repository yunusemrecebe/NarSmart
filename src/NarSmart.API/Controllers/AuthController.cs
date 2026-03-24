using MediatR;
using Microsoft.AspNetCore.Mvc;
using NarSmart.Application.Features.Auth.Commands.Login;
using NarSmart.Application.Features.Auth.Queries.GetUserHotels;

namespace NarSmart.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator) => _mediator = mediator;

    [HttpPost("hotels")]
    public async Task<IActionResult> GetUserHotels([FromBody] GetUserHotelsQuery query)
    {
        var result = await _mediator.Send(query);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : Unauthorized(result);
    }
}
