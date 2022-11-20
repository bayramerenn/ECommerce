using ECommerceCommon.EventBusModel;
using MediatR;

namespace ECommerceOrder.Features.Command
{
    public class CreateOrderRequest : IRequest<CreateOrderResponse>
    {
        public OrderCreateEvent[] Data { get; set; }
    }
}