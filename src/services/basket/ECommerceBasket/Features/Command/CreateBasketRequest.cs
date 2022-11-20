using ECommerceBasket.Dtos;
using MediatR;

namespace ECommerceBasket.Features.Command
{
    public class CreateBasketRequest : IRequest<CreateBasketResponse>
    {
        public BasketDto[] Data { get; set; }
    }
}