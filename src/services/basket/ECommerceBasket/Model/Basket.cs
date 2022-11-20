namespace ECommerceBasket.Model
{
    public class Basket
    {
        public string BasketId { get; set; }
        public int CustomerId { get; set; }
        public string ProductName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}