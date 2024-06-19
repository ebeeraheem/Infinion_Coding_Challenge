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
                }
                );
        });
    }
}

