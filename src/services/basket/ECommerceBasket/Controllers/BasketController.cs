using ECommerceBasket.Features.Command;
using ECommerceBasket.Features.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBasket.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Get")]
        public async Task<IActionResult> Get(GetByIdOrderRequest getByIdOrderRequest)
        {
            return Ok(await _mediator.Send(getByIdOrderRequest));
        }

        [HttpPost("Save")]
        public async Task<IActionResult> Save(CreateBasketRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder(CreateOrderRequest createOrderRequest)
        {
            return Ok(await _mediator.Send(createOrderRequest));
        }
    }
}