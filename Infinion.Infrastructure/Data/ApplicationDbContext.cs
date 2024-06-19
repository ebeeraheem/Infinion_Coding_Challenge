using Infinion.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infinion.Infrastructure.Data;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
        base(options)
    {
    }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Price)
                  .HasColumnType("decimal(18,2)");

            entity.HasData(
                new Product
                {
                    Id = 1,
                    Name = "Laptop",
                    Description = "High performance laptop.",
                    Price = 150000.00m,
                    Stock = 50,
                    Category = "Electronics",
                    ImageUrl = new Uri("https://example.com/images/laptop.jpg"),
                    CreatedAt = DateTime.UtcNow,
                    LastUpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = 2,
                    Name = "Smartphone",
                    Description = "Latest model smartphone.",
                    Price = 80000.00m,
                    Stock = 100,
                    Category = "Electronics",
                    ImageUrl = new Uri("https://example.com/images/smartphone.jpg"),
                    CreatedAt = DateTime.UtcNow,
                    LastUpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = 3,
                    Name = "Office Chair",
                    Description = "Comfortable ergonomic office chair.",
                    Price = 20000.00m,
                    Stock = 200,
                    Category = "Furniture",
                    ImageUrl = new Uri("https://example.com/images/office-chair.jpg"),
                    CreatedAt = DateTime.UtcNow,
                    LastUpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = 4,
                    Name = "Desk Lamp",
                    Description = "Stylish desk lamp with adjustable brightness.",
                    Price = 5000.00m,
                    Stock = 300,
                    Category = "Home Decor",
                    ImageUrl = new Uri("https://example.com/images/desk-lamp.jpg"),
                    CreatedAt = DateTime.UtcNow,
                    LastUpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = 5,
                    Name = "Bluetooth Headphones",
                    Description = "Noise-cancelling over-ear headphones.",
                    Price = 25000.00m,
                    Stock = 150,
                    Category = "Electronics",
                    ImageUrl = new Uri("https://example.com/images/bluetooth-headphones.jpg"),
                    CreatedAt = DateTime.UtcNow,
                    LastUpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = 6,
                    Name = "Coffee Maker",
                    Description = "Automatic coffee maker with programmable settings.",
                    Price = 25000.00m,
                    Stock = 80,
                    Category = "Home Appliances",
                    ImageUrl = new Uri("https://example.com/images/coffee-maker.jpg"),
                    CreatedAt = DateTime.UtcNow,
                    LastUpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = 7,
                    Name = "Electric Guitar",
                    Description = "Electric guitar with solid body and maple neck.",
                    Price = 45000.00m,
                    Stock = 30,
                    Category = "Musical Instruments",
                    ImageUrl = new Uri("https://example.com/images/electric-guitar.jpg"),
                    CreatedAt = DateTime.UtcNow,
                    LastUpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = 8,
                    Name = "Smart Watch",
                    Description = "Smart watch with health and fitness tracking features.",
                    Price = 30000.00m,
                    Stock = 120,
                    Category = "Electronics",
                    ImageUrl = new Uri("https://example.com/images/smart-watch.jpg"),
                    CreatedAt = DateTime.UtcNow,
                    LastUpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = 9,
                    Name = "Blender",
                    Description = "High-speed blender with multiple settings.",
                    Price = 18000.00m,
                    Stock = 60,
                    Category = "Kitchen Appliances",
                    ImageUrl = new Uri("https://example.com/images/blender.jpg"),
                    CreatedAt = DateTime.UtcNow,
                    LastUpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = 10,
                    Name = "Mountain Bike",
                    Description = "Durable mountain bike with 21-speed gear system.",
                    Price = 60000.00m,
                    Stock = 25,
                    Category = "Sports",
                    ImageUrl = new Uri("https://example.com/images/mountain-bike.jpg"),
                    CreatedAt = DateTime.UtcNow,
                    LastUpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = 11,
                    Name = "Bluetooth Speaker",
                    Description = "Portable Bluetooth speaker with high-quality sound.",
                    Price = 12000.00m,
                    Stock = 300,
                    Category = "Electronics",
                    ImageUrl = new Uri("https://example.com/images/bluetooth-speaker.jpg"),
                    CreatedAt = DateTime.UtcNow,
                    LastUpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = 12,
                    Name = "Running Shoes",
                    Description = "Lightweight and comfortable running shoes.",
                    Price = 5000.00m,
                    Stock = 100,
                    Category = "Footwear",
                    ImageUrl = new Uri("https://example.com/images/running-shoes.jpg"),
                    CreatedAt = DateTime.UtcNow,
                    LastUpdatedAt = DateTime.UtcNow
                }
                );
        });
    }
}

