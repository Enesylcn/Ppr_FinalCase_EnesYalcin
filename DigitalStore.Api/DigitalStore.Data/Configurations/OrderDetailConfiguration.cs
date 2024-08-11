using DigitalStore.Data.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Data.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.InsertDate).IsRequired(true);
            builder.Property(x => x.IsActive).IsRequired(true);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.InsertUser).IsRequired().HasMaxLength(50);
            builder.Property(od => od.UnitPrice).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(od => od.TotalPrice).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(od => od.Stock).IsRequired().HasColumnType("smallint");
            builder.Property(od => od.Quantity).IsRequired().HasColumnType("smallint");

            builder.HasOne(od => od.Order)
                   .WithMany(o => o.OrderDetails)
                   .HasForeignKey(od => od.OrderId);

            builder.HasOne(od => od.Product)
                   .WithMany()
                   .HasForeignKey(od => od.ProductId);

            builder.ToTable("OrderDetails");
        }
    }
}
