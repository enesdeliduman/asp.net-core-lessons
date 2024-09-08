using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductsAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddFullNameToAppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", null, "Admin", "ADMIN" },
                    { "2", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateAdded", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1", 0, "e8b55bb6-4d8a-4b26-882f-4d987c9c1d7f", new DateTime(2024, 9, 8, 19, 5, 41, 873, DateTimeKind.Local).AddTicks(499), "admin@products.com", true, "Admin User", false, null, "ADMIN@PRODUCTS.COM", "ADMIN@PRODUCTS.COM", "AQAAAAIAAYagAAAAEOI+kHZrdwk6hIjRdkpckL5LbRUK7ao/QEtO1LwZ6M4Ip/ns9vuFwIK2ul/9U+/PYA==", null, false, "", false, "admin@products.com" },
                    { "2", 0, "0578161b-3df5-4297-91a7-07af2a110c7c", new DateTime(2024, 9, 8, 19, 5, 41, 952, DateTimeKind.Local).AddTicks(3901), "user@products.com", true, "Regular User", false, null, "USER@PRODUCTS.COM", "USER@PRODUCTS.COM", "AQAAAAIAAYagAAAAEMSZl/J4vN6ggKB6D0iHO5Pq2YMyXKeIHFwMVhUGTsE4jHDDOFF/Cx7yBZ0VwFSlxA==", null, false, "", false, "user@products.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1", "1" },
                    { "2", "2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "2" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2");
        }
    }
}
