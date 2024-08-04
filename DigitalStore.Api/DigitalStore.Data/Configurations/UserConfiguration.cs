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
            builder.Property(x => x.InsertDate).IsRequired(true);
            builder.Property(x => x.IsActive).IsRequired(true);

            builder.Property(x => x.FirstName).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.LastName).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.Email).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.Role).IsRequired(true).HasMaxLength(10);
            builder.Property(x => x.Password).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.LastLoginDate).IsRequired(false);
            builder.Property(x => x.Status).IsRequired(true);
            builder.Property(x => x.PointCash).IsRequired(true);

            builder.HasIndex(x => new { x.UserName }).IsUnique(true);
            builder.HasIndex(x => new { x.Email }).IsUnique(true);
        }
    }
}
