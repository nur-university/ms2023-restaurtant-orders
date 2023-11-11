using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Order.Application.UseCases.CreateOrder;

namespace Restaurant.Order.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : Controller
{
    private readonly IMediator _mediator;
    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    
    [HttpPost]
    public async Task<IActionResult> InsertOrden([FromBody] CreateOrderCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
