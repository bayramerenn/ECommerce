using ECommerceOrder.Models;
using ECommerceOrder.Persistence.Mappings;
using Microsoft.EntityFrameworkCore;

namespace ECommerceOrder.Persistence
{
    public class ECommerceContext : DbContext
    {
        public ECommerceContext(DbContextOptions<ECommerceContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderMap());
            modelBuilder.ApplyConfiguration(new LogMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}