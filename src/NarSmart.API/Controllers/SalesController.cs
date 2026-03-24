using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NarSmart.Application.Features.Sales.Commands.AddGuestToSale;
using NarSmart.Application.Features.Sales.Commands.CreateSale;
using NarSmart.Application.Features.Sales.Queries.GetSaleById;
using NarSmart.Application.Features.Sales.Queries.GetSales;

namespace NarSmart.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SalesController : ControllerBase
{
    private readonly IMediator _mediator;

    public SalesController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetSalesQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetSaleByIdQuery(id));
        return result.IsSuccess ? Ok(result) : NotFound(result);
    }

    [HttpPost]
    [Authorize(Roles = "Receptionist,Manager,SystemAdmin")]
    public async Task<IActionResult> Create([FromBody] CreateSaleCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { id = result.Data }, result) : BadRequest(result);
    }

    [HttpPost("{saleId:guid}/guests")]
    [Authorize(Roles = "Receptionist,Manager,SystemAdmin")]
    public async Task<IActionResult> AddGuest(Guid saleId, [FromBody] AddGuestToSaleCommand command)
    {
        if (saleId != command.SaleId)
            return BadRequest("Route saleId and body saleId do not match.");

        var result = await _mediator.Send(command);
        return result.IsSuccess ? Created(string.Empty, result) : BadRequest(result);
    }
}
