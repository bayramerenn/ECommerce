using ECommerceOrder.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceOrder.Persistence.Mappings
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.CustomerId);
            builder.Property(x => x.ProductName).HasColumnType("NVARCHAR(MAX)");
            builder.Property(x => x.CreatedAt);
            builder.Property(x => x.Quantity);
            builder.Property(x => x.Price);
            builder.ToTable("Orders");
        }
    }
}