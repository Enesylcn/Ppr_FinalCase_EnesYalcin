using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DigitalStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class OrderTableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d291eeca-ed50-4fc0-9a6a-6341714406f8", "46410a68-6610-4d1c-9ed0-db0b62e22b5f" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3e508b91-d22e-42b1-8469-eec160242bdf", "850bf6b8-1fce-49a6-a208-17752ccb7519" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3e508b91-d22e-42b1-8469-eec160242bdf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d291eeca-ed50-4fc0-9a6a-6341714406f8");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "46410a68-6610-4d1c-9ed0-db0b62e22b5f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "850bf6b8-1fce-49a6-a208-17752ccb7519");

            migrationBuilder.AlterColumn<short>(
                name: "Stock",
                table: "OrderDetails",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<short>(
                name: "Quantity",
                table: "OrderDetails",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "49208790-a4ee-4282-a4a2-cf9a831fc963", null, "Admin", null },
                    { "b1b3568a-7a9f-4200-8562-a86ee279c17d", null, "Customer", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "City", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "Gender", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Occupation", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "905f14fe-1e47-4c7e-8851-e1f0c1d0f380", 0, "Nokta Mah. Virgül Caddesi Ünlem Sokak no:1 daire:2", "İstanbul", "8c8ee37b-250a-4200-9527-b496cb24afb1", new DateTime(1998, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "customer@gmail.com", true, "customer", "Unisex", "customer", false, null, "CUSTOMER@GMAIL.COM", "CUSTOMER", "Customer", "AQAAAAIAAYagAAAAELzBt3EgvMHeR8Ek/GVRbNUyvmUPFll68xvQe85aTOkuYQbVuVFM+Z+q6SH5DLQKHg==", "05687654321", false, "7bb3f853-ab34-47a4-8b70-b7a740ec94e7", false, "customer" },
                    { "9b8072be-6cb7-4d7c-b595-43a23bcd44db", 0, "Nokta Mah. Virgül Caddesi Ünlem Sokak no:1 daire:2", "İstanbul", "4064ef65-9dab-4ad6-9ad0-b2f55244e390", new DateTime(1999, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "enes@gmail.com", true, "Enes", "Erkek", "Yalçın", false, null, "ENES@GMAIL.COM", "ENES", "Software Dev.", "AQAAAAIAAYagAAAAEEX/4WJXy3UgV1pfnRRs9FIQQOptw1q2G+yndUO53HgSoIbo3CbQeZON9gHxvlQNDg==", "05387654321", false, "e4a29951-f536-4f01-aa3a-31d8a922b597", false, "Enes" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "b1b3568a-7a9f-4200-8562-a86ee279c17d", "905f14fe-1e47-4c7e-8851-e1f0c1d0f380" },
                    { "49208790-a4ee-4282-a4a2-cf9a831fc963", "9b8072be-6cb7-4d7c-b595-43a23bcd44db" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b1b3568a-7a9f-4200-8562-a86ee279c17d", "905f14fe-1e47-4c7e-8851-e1f0c1d0f380" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "49208790-a4ee-4282-a4a2-cf9a831fc963", "9b8072be-6cb7-4d7c-b595-43a23bcd44db" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "49208790-a4ee-4282-a4a2-cf9a831fc963");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b1b3568a-7a9f-4200-8562-a86ee279c17d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "905f14fe-1e47-4c7e-8851-e1f0c1d0f380");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9b8072be-6cb7-4d7c-b595-43a23bcd44db");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "OrderDetails");

            migrationBuilder.AlterColumn<int>(
                name: "Stock",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3e508b91-d22e-42b1-8469-eec160242bdf", null, "Customer", null },
                    { "d291eeca-ed50-4fc0-9a6a-6341714406f8", null, "Admin", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "City", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "Gender", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Occupation", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "46410a68-6610-4d1c-9ed0-db0b62e22b5f", 0, "Nokta Mah. Virgül Caddesi Ünlem Sokak no:1 daire:2", "İstanbul", "5cef0949-eaa3-4225-82c5-73c9c72f51de", new DateTime(1999, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "enes@gmail.com", true, "Enes", "Erkek", "Yalçın", false, null, "ENES@GMAIL.COM", "ENES", "Software Dev.", "AQAAAAIAAYagAAAAEKixhGkgDi9cT23qpXHvtLlYwQ+AKlT1LL4Hvdxv6yJG90k0/EgHj/icj+ZF4jEa7Q==", "05387654321", false, "696c56ea-f855-49a0-a272-8e0f46f3e4e5", false, "Enes" },
                    { "850bf6b8-1fce-49a6-a208-17752ccb7519", 0, "Nokta Mah. Virgül Caddesi Ünlem Sokak no:1 daire:2", "İstanbul", "07d5f9ff-e003-4612-b332-2a4691c3c63e", new DateTime(1998, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "customer@gmail.com", true, "customer", "Unisex", "customer", false, null, "CUSTOMER@GMAIL.COM", "CUSTOMER", "Customer", "AQAAAAIAAYagAAAAEBkso21e6adQYSO2n13e8QUVTQT/NhlHSSsXPgxr/ndqqFQvGFxDjPz3o2glz+zgEQ==", "05687654321", false, "18261330-8406-4ffc-8bda-5c30022fc190", false, "customer" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "d291eeca-ed50-4fc0-9a6a-6341714406f8", "46410a68-6610-4d1c-9ed0-db0b62e22b5f" },
                    { "3e508b91-d22e-42b1-8469-eec160242bdf", "850bf6b8-1fce-49a6-a208-17752ccb7519" }
                });
        }
    }
}
