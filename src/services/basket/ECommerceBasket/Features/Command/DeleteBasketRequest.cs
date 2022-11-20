using MediatR;

namespace ECommerceBasket.Features.Command
{
    public class DeleteBasketRequest : IRequest<DeleteBasketResponse>
    {
        public string BasketId { get; set; }
    }
}