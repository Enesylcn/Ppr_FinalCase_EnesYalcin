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
    public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.InsertDate).IsRequired(true);
            builder.Property(x => x.IsActive).IsRequired(true);
            builder.Property(c => c.Name).IsRequired(false).HasMaxLength(100);
            builder.Property(c => c.InsertUser).IsRequired(false).HasMaxLength(50);
            builder.Property(o => o.UserId).IsRequired(true);
            builder.Property(p => p.TotalAmount).HasColumnType("float");
            builder.Property(p => p.CartAmount).HasColumnType("float");
            builder.Property(p => p.PointsAmount).HasColumnType("float");
            builder.Property(p => p.CouponCode).HasMaxLength(50);

            // Configure relationships
            builder.HasMany(scı => scı.ShoppingCartItems)
               .WithOne(sc => sc.ShoppingCart)
               .HasForeignKey(sc => sc.ShoppingCartId);

            builder.ToTable("Carts");
        }
    }
}
