using MediatR;

namespace ECommerceBasket.Features.Queries
{
    public class GetByIdOrderRequest : IRequest<GetByIdOrderResponse>
    {
        public string Id { get; set; }
    }
}