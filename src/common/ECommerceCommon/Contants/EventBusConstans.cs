namespace ECommerceCommon.Contants
{
    public static class EventBusConstans
    {
        public static string QueueCreateOrderSend = "queue:create-order-service";
        public static string QueueCreateOrderConsumer = "create-order-service";
        public static string QueueDeleteBasketSend = "queue:delete-basket-service";
        public static string QueueDeleteBasketConsumer = "delete-basket-service";
    }
}