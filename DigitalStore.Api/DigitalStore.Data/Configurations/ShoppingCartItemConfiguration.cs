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
    public class ShoppingCartItemConfiguration : IEntityTypeConfiguration<ShoppingCartItem>
    {
        public void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.InsertDate).IsRequired(true);
            builder.Property(x => x.IsActive).IsRequired(true);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.InsertUser).IsRequired().HasMaxLength(50);
            builder.Property(o => o.ProductId).IsRequired(true);
            builder.Property(o => o.ShoppingCartId).IsRequired(true);
            builder.Property(p => p.Stock).HasColumnType("int");

            builder.ToTable("CartItems");
        }
    }
}
