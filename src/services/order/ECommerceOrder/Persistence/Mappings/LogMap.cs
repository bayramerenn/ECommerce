using ECommerceOrder.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceOrder.Persistence.Mappings
{
    public class LogMap : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Message).HasColumnType("NVARCHAR(MAX)");
            builder.Property(x => x.MessageTemplate).HasColumnType("NVARCHAR(MAX)");
            builder.Property(x => x.Level).HasColumnType("nvarchar(128)");
            builder.Property(x => x.TimeStamp).HasColumnType("datetime");
            builder.Property(x => x.Exception).HasColumnType("NVARCHAR(MAX)");
            builder.Property(x => x.Properties).HasColumnType("NVARCHAR(MAX)");
            builder.ToTable("Logs");
        }
    }
}
