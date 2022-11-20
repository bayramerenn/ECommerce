using ECommerceBasket.Dtos;

namespace ECommerceBasket.Features.Queries
{
    public class GetByIdOrderResponse
    {
        public BasketDto[] Basket { get; set; }
    }
}