using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NarSmart.Application.Features.Users.Commands.CreateUser;
using NarSmart.Application.Features.Users.Commands.DeleteUser;
using NarSmart.Application.Features.Users.Commands.UpdateUser;
using NarSmart.Application.Features.Users.Queries.GetUserById;
using NarSmart.Application.Features.Users.Queries.GetUsers;

namespace NarSmart.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "SystemAdmin,Manager")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetUsersQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetUserByIdQuery(id));
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { id = result.Data }, result) : BadRequest(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserCommand command)
    {
        if (id != command.Id)
            return BadRequest("Route id and body id do not match.");

        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, [FromBody] DeleteUserCommand command)
    {
        if (id != command.Id)
            return BadRequest("Route id and body id do not match.");

        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }
}
