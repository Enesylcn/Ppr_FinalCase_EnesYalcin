using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalStore.Data.Domain;

namespace DigitalStore.Data.Configurations
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.HasKey(pcm => new { pcm.CategoryId, pcm.ProductId });

            builder.HasOne(pcm => pcm.Category)
                   .WithMany()
                   .HasForeignKey(pcm => pcm.CategoryId);

            builder.HasOne(pcm => pcm.Product)
                   .WithMany()
                   .HasForeignKey(pcm => pcm.ProductId);

            builder.ToTable("ProductCategory");
        }
    }
}
