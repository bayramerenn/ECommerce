using ECommerceOrder.Features.Command;
using ECommerceOrder.Features.Queries;
using ECommerceOrder.Models;
using ECommerceOrder.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediator.Send(new GetListOrderRequest()));
        }

    }
}
