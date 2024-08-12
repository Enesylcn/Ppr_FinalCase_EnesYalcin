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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(u => u.LastName).IsRequired().HasMaxLength(50);
            builder.Property(o => o.PhoneNumber).IsRequired().HasMaxLength(11);
            builder.Property(u => u.Address).HasMaxLength(300);
            builder.Property(u => u.City).HasMaxLength(50);
            builder.Property(u => u.Gender).HasMaxLength(10);
            builder.Property(u => u.DateOfBirth).IsRequired(false);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Occupation).HasMaxLength(50);

            builder.ToTable("Users");
    }
}
}
