using DigitalStore.Data.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DigitalStore.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.CartAmount).HasColumnType("decimal(18,2)");
            builder.Property(o => o.CouponAmount).HasColumnType("decimal(18,2)");
            builder.Property(o => o.CouponCode).HasMaxLength(50);
            builder.Property(o => o.PointsAmount).HasColumnType("decimal(18,2)");

            builder.ToTable("Orders");
        }
    }
}
