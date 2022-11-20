namespace ECommerceCommon.EventBusModel
{
    public class OrderCreateEventResource
    {
        public string BasketId { get; set; }
        public OrderCreateEvent[] OrderCreateEvents { get; set; }
    }

    public class OrderCreateEvent
    {
        public int CustomerId { get; set; }
        public string ProductName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}