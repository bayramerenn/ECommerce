using StackExchange.Redis;

namespace ECommerceBasket.Services
{
    public class RedisService
    {
        private readonly string _host;
        private ConnectionMultiplexer _ConnectionMultiplexer;

        public RedisService(string host)
        {
            _host = host;
        }

        public void Connect() => _ConnectionMultiplexer ??= ConnectionMultiplexer.Connect(_host);

        public IDatabase GetDb(int db = 1) => _ConnectionMultiplexer.GetDatabase(db);
    }
}