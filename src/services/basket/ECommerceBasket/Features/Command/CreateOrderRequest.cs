using MediatR;

namespace ECommerceBasket.Features.Command
{
    public class CreateOrderRequest: IRequest<CreateOrderResponse>
    {
        public string BasketId { get; set; }
    }
}
