using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infinion.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedProductsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "CreatedAt", "Description", "ImageUrl", "LastUpdatedAt", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { 1, "Electronics", new DateTime(2024, 6, 19, 15, 2, 32, 727, DateTimeKind.Utc).AddTicks(3537), "High performance laptop.", "https://example.com/images/laptop.jpg", new DateTime(2024, 6, 19, 15, 2, 32, 727, DateTimeKind.Utc).AddTicks(3540), "Laptop", 150000.00m, 50 },
                    { 2, "Electronics", new DateTime(2024, 6, 19, 15, 2, 32, 727, DateTimeKind.Utc).AddTicks(3550), "Latest model smartphone.", "https://example.com/images/smartphone.jpg", new DateTime(2024, 6, 19, 15, 2, 32, 727, DateTimeKind.Utc).AddTicks(3551), "Smartphone", 80000.00m, 100 },
                    { 3, "Furniture", new DateTime(2024, 6, 19, 15, 2, 32, 727, DateTimeKind.Utc).AddTicks(3559), "Comfortable ergonomic office chair.", "https://example.com/images/office-chair.jpg", new DateTime(2024, 6, 19, 15, 2, 32, 727, DateTimeKind.Utc).AddTicks(3559), "Office Chair", 20000.00m, 200 },
                    { 4, "Home Decor", new DateTime(2024, 6, 19, 15, 2, 32, 727, DateTimeKind.Utc).AddTicks(3568), "Stylish desk lamp with adjustable brightness.", "https://example.com/images/desk-lamp.jpg", new DateTime(2024, 6, 19, 15, 2, 32, 727, DateTimeKind.Utc).AddTicks(3569), "Desk Lamp", 5000.00m, 300 },
                    { 5, "Electronics", new DateTime(2024, 6, 19, 15, 2, 32, 727, DateTimeKind.Utc).AddTicks(3576), "Noise-cancelling over-ear headphones.", "https://example.com/images/bluetooth-headphones.jpg", new DateTime(2024, 6, 19, 15, 2, 32, 727, DateTimeKind.Utc).AddTicks(3576), "Bluetooth Headphones", 25000.00m, 150 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
