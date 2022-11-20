using ECommerceBasket.Features.Command;
using ECommerceCommon.EventBusModel;
using MassTransit;
using MediatR;

namespace ECommerceBasket.Consumers
{
    public class BasketDeleteConsumer : IConsumer<BasketDeleteEvent>
    {
        private readonly IMediator _mediator;

        public BasketDeleteConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<BasketDeleteEvent> context)
        {
            await _mediator.Send(new DeleteBasketRequest { BasketId = context.Message.BasketId });
        }
    }
}