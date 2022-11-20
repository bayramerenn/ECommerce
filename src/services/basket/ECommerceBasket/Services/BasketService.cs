using ECommerceBasket.Model;
using System.Text.Json;

namespace ECommerceBasket.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<bool> Save(Basket[] basket)
        {
            var data = JsonSerializer.Serialize(new { UId = Guid.NewGuid().ToString(), Basket = basket });
            string basketId = basket.FirstOrDefault().BasketId;
            var result = await _redisService.GetDb().StringSetAsync(basketId, JsonSerializer.Serialize(basket));
            return result;
        }

        public async Task<bool> Delete(string id)
        {
            var result = await _redisService.GetDb().KeyDeleteAsync(id);
            return result;
        }

        public async Task<Basket[]> Get(string id)
        {
            var result = await _redisService.GetDb().StringGetAsync(id);
            if (result.HasValue)
            {
                return JsonSerializer.Deserialize<Basket[]>(result);
            }
            return new Basket[0];
        }
    }
}