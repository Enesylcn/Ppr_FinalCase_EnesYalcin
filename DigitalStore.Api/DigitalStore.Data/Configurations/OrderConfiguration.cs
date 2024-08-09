using DigitalStore.Data.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DigitalStore.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(x => x.InsertDate).IsRequired(true);
            builder.Property(x => x.IsActive).IsRequired(true);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.InsertUser).IsRequired().HasMaxLength(50);
            builder.Property(o => o.OrderNumber).HasMaxLength(50);
            builder.Property(o => o.OrderDate).IsRequired(true);
            builder.Property(o => o.UserId).IsRequired(true);
            builder.Property(o => o.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(o => o.LastName).IsRequired().HasMaxLength(50);
            builder.Property(o => o.Address).IsRequired().HasMaxLength(350);
            builder.Property(o => o.City).IsRequired().HasMaxLength(50);
            builder.Property(o => o.PhoneNumber).IsRequired().HasMaxLength(11);
            builder.Property(o => o.Email).IsRequired().HasMaxLength(50);
            builder.Property(o => o.Note).HasMaxLength(600);

            builder.HasMany(o => o.OrderDetails)
           .WithOne(o => o.Order)
           .HasForeignKey(o => o.OrderId)
           .IsRequired(true)

           .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Orders");
        }
    }
}
