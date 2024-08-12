using DigitalStore.Data.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DigitalStore.Data.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            #region Roles

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole{Name = "Admin"},
                new IdentityRole{Name = "Customer"}
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
            #endregion
            #region Users
            List<User> users = new List<User>
            {
                new User
                {
                    FirstName = "Enes",
                    LastName = "Yalçın",
                    UserName = "Enes",
                    NormalizedUserName = "ENES",
                    Email = "enes@gmail.com",
                    NormalizedEmail = "ENES@GMAIL.COM",
                    Gender = "Erkek",
                    DateOfBirth = new DateTime(1999,10,29),
                    Address = "Nokta Mah. Virgül Caddesi Ünlem Sokak no:1 daire:2",
                    City = "İstanbul",
                    PhoneNumber = "05387654321",
                    EmailConfirmed = true,
                    Occupation = "Software Dev.",
                    PointsWallet = 0
                },
                new User
                {
                    FirstName = "customer",
                    LastName = "customer",
                    UserName = "customer",
                    NormalizedUserName = "CUSTOMER",
                    Email = "customer@gmail.com",
                    NormalizedEmail = "CUSTOMER@GMAIL.COM",
                    Gender = "Unisex",
                    DateOfBirth = new DateTime(1998,4,20),
                    Address = "Nokta Mah. Virgül Caddesi Ünlem Sokak no:1 daire:2",
                    City = "İstanbul",
                    PhoneNumber = "05687654321",
                    EmailConfirmed = true,
                    Occupation = "Customer",
                    PointsWallet = 0
                }
            };
            modelBuilder.Entity<User>().HasData(users);
            #endregion
            #region Password
            var passwordHasher = new PasswordHasher<User>();
            users[0].PasswordHash = passwordHasher.HashPassword(users[0], "Qwe123.");
            users[1].PasswordHash = passwordHasher.HashPassword(users[1], "Qwe123.");

            #endregion
            #region Role Assignment
            List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    UserId = users[0].Id,
                    RoleId = roles[0].Id,
                },
                new IdentityUserRole<string>
                {
                    UserId = users[1].Id,
                    RoleId = roles[1].Id,
                }
            };
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRoles);
            #endregion
        }
    }
}
