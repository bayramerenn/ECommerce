using ECommerceCommon.Contants;
using ECommerceCommon.EventBusModel;
using ECommerceCommon.Exceptions;
using ECommerceOrder.Features.Command;
using MassTransit;
using MediatR;

namespace ECommerceOrder.Consumers
{
    public class OrderCreateConsumer : IConsumer<OrderCreateEventResource>
    {
        private readonly IMediator _mediator;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public OrderCreateConsumer(IMediator mediator, ISendEndpointProvider sendEndpointProvider = null)
        {
            _mediator = mediator;
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task Consume(ConsumeContext<OrderCreateEventResource> context)
        {
            await _mediator.Send(new CreateOrderRequest { Data = context.Message.OrderCreateEvents });

            try
            {
                var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri(EventBusConstans.QueueDeleteBasketSend));
                await sendEndpoint.Send<BasketDeleteEvent>(new BasketDeleteEvent { BasketId = context.Message.BasketId });
            }
            catch
            {
                throw new RestException(System.Net.HttpStatusCode.BadRequest, new { CurrentAccount = Messages.EventBasketDeleteError });
            }
        }
    }
}