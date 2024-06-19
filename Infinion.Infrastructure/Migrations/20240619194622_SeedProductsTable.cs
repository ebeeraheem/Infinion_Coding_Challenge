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
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "CreatedAt", "Description", "ImageUrl", "LastUpdatedAt", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { 1, "Electronics", new DateTime(2024, 6, 19, 19, 46, 21, 568, DateTimeKind.Utc).AddTicks(8989), "High performance laptop.", "https://example.com/images/laptop.jpg", new DateTime(2024, 6, 19, 19, 46, 21, 568, DateTimeKind.Utc).AddTicks(8992), "Laptop", 150000.00m, 50 },
                    { 2, "Electronics", new DateTime(2024, 6, 19, 19, 46, 21, 568, DateTimeKind.Utc).AddTicks(9004), "Latest model smartphone.", "https://example.com/images/smartphone.jpg", new DateTime(2024, 6, 19, 19, 46, 21, 568, DateTimeKind.Utc).AddTicks(9005), "Smartphone", 80000.00m, 100 },
                    { 3, "Furniture", new DateTime(2024, 6, 19, 19, 46, 21, 568, DateTimeKind.Utc).AddTicks(9015), "Comfortable ergonomic office chair.", "https://example.com/images/office-chair.jpg", new DateTime(2024, 6, 19, 19, 46, 21, 568, DateTimeKind.Utc).AddTicks(9016), "Office Chair", 20000.00m, 200 },
                    { 4, "Home Decor", new DateTime(2024, 6, 19, 19, 46, 21, 568, DateTimeKind.Utc).AddTicks(9023), "Stylish desk lamp with adjustable brightness.", "https://example.com/images/desk-lamp.jpg", new DateTime(2024, 6, 19, 19, 46, 21, 568, DateTimeKind.Utc).AddTicks(9024), "Desk Lamp", 5000.00m, 300 },
                    { 5, "Electronics", new DateTime(2024, 6, 19, 19, 46, 21, 568, DateTimeKind.Utc).AddTicks(9031), "Noise-cancelling over-ear headphones.", "https://example.com/images/bluetooth-headphones.jpg", new DateTime(2024, 6, 19, 19, 46, 21, 568, DateTimeKind.Utc).AddTicks(9032), "Bluetooth Headphones", 25000.00m, 150 },
                    { 6, "Home Appliances", new DateTime(2024, 6, 19, 19, 46, 21, 568, DateTimeKind.Utc).AddTicks(9039), "Automatic coffee maker with programmable settings.", "https://example.com/images/coffee-maker.jpg", new DateTime(2024, 6, 19, 19, 46, 21, 568, DateTimeKind.Utc).AddTicks(9039), "Coffee Maker", 25000.00m, 80 },
                    { 7, "Musical Instruments", new DateTime(2024, 6, 19, 19, 46, 21, 568, DateTimeKind.Utc).AddTicks(9046), "Electric guitar with solid body and maple neck.", "https://example.com/images/electric-guitar.jpg", new DateTime(2024, 6, 19, 19, 46, 21, 568, DateTimeKind.Utc).AddTicks(9047), "Electric Guitar", 45000.00m, 30 },
                    { 8, "Electronics", new DateTime(2024, 6, 19, 19, 46, 21, 568, DateTimeKind.Utc).AddTicks(9054), "Smart watch with health and fitness tracking features.", "https://example.com/images/smart-watch.jpg", new DateTime(2024, 6, 19, 19, 46, 21, 568, DateTimeKind.Utc).AddTicks(9055), "Smart Watch", 30000.00m, 120 },
                    { 9, "Kitchen Appliances", new DateTime(2024, 6, 19, 19, 46, 21, 568, DateTimeKind.Utc).AddTicks(9062), "High-speed blender with multiple settings.", "https://example.com/images/blender.jpg", new DateTime(2024, 6, 19, 19, 46, 21, 568, DateTimeKind.Utc).AddTicks(9062), "Blender", 18000.00m, 60 },
                    { 10, "Sports", new DateTime(2024, 6, 19, 19, 46, 21, 568, DateTimeKind.Utc).AddTicks(9069), "Durable mountain bike with 21-speed gear system.", "https://example.com/images/mountain-bike.jpg", new DateTime(2024, 6, 19, 19, 46, 21, 568, DateTimeKind.Utc).AddTicks(9070), "Mountain Bike", 60000.00m, 25 },
                    { 11, "Electronics", new DateTime(2024, 6, 19, 19, 46, 21, 568, DateTimeKind.Utc).AddTicks(9077), "Portable Bluetooth speaker with high-quality sound.", "https://example.com/images/bluetooth-speaker.jpg", new DateTime(2024, 6, 19, 19, 46, 21, 568, DateTimeKind.Utc).AddTicks(9078), "Bluetooth Speaker", 12000.00m, 300 },
                    { 12, "Footwear", new DateTime(2024, 6, 19, 19, 46, 21, 568, DateTimeKind.Utc).AddTicks(9084), "Lightweight and comfortable running shoes.", "https://example.com/images/running-shoes.jpg", new DateTime(2024, 6, 19, 19, 46, 21, 568, DateTimeKind.Utc).AddTicks(9085), "Running Shoes", 5000.00m, 100 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
