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
    public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.InsertDate).IsRequired(true);
            builder.Property(x => x.IsActive).IsRequired(true);
            builder.Property(c => c.Name).HasMaxLength(100);
            builder.Property(c => c.InsertUser).IsRequired().HasMaxLength(50);

            builder.Property(c => c.Code).IsRequired().HasMaxLength(10);
            builder.Property(c => c.ValidFrom).IsRequired(true);
            builder.Property(c => c.ValidUntil).IsRequired(true);


            builder.ToTable("Coupons");
        }
    }
}
