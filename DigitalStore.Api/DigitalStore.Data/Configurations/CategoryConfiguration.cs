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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.InsertDate).IsRequired(true);
            builder.Property(x => x.IsActive).IsRequired(true);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.InsertUser).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Url).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Tags).HasMaxLength(200);

            builder.HasIndex(c => c.Name)
                    .IsUnique();

            builder.HasMany(x => x.ProductCategories)
           .WithOne(x => x.Category)
           .HasForeignKey(x => x.CategoryId)
           .IsRequired(true)
           .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Categories");
        }
    }
}
