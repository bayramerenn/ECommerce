namespace ECommerceOrder.Persistence
{
    public class DbInitializer
    {
        private readonly ECommerceContext _context;

        public DbInitializer(ECommerceContext context)
        {
            _context = context;
        }

        public void Run()
        {
            _context.Database.EnsureCreated();
        }
    }
}