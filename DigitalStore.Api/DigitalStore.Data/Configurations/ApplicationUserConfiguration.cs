using DigitalStore.Data.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DigitalStore.Data.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {

            builder.HasKey(u => u.Id);
            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Address)
                .HasMaxLength(100);

            builder.Property(u => u.City)
                .HasMaxLength(50);

            builder.Property(u => u.Gender)
                .HasMaxLength(10);

            builder.Property(u => u.DateOfBirth)
                .IsRequired(false);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Occupation)
                .HasMaxLength(50);

            builder.Property(u => u.PointCash)
                .HasColumnType("decimal(18,2)");

            builder.HasMany(u => u.Orders)
                .WithOne(o => o.ApplicationUser)
                .HasForeignKey(o => o.AppUserId);

            builder.ToTable("ApplicationUsers");
        }
    }
}
