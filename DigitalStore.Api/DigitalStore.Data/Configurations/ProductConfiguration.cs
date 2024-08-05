﻿using DigitalStore.Data.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Features).HasMaxLength(500);
            builder.Property(p => p.Description).HasMaxLength(1000);
            builder.Property(p => p.PointsEarningPercentage).HasColumnType("decimal(5,2)");
            builder.Property(p => p.MaxPointsAmount).HasColumnType("decimal(18,2)");

            // Configure relationships
            builder.HasMany(p => p.ProductCategories)
                .WithOne(pc => pc.Product)
                .HasForeignKey(pc => pc.ProductId);

            builder.HasMany(p => p.OrderDetails)
                .WithOne(od => od.Product)
                .HasForeignKey(od => od.ProductId);

            builder.ToTable("Products");
        }
    }
}
