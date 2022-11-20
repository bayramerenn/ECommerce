using ECommerceBasket.Model;

namespace ECommerceBasket.Dtos
{
    public class BasketListDto
    {
        public Guid UId { get; set; }
        public List<Basket> basket { get; set; }
    }
}