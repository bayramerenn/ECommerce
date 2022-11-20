using ECommerceOrder.Dtos;

namespace ECommerceOrder.Features.Queries
{
    public class GetListOrderResponse
    {
        public List<OrderDto> Data { get; set; }
    }
}