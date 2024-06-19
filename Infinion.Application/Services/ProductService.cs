using Infinion.Application.Services.Interfaces;
using Infinion.Domain.DTOs;
using Infinion.Domain.Entities;
using Infinion.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infinion.Application.Services;
public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;

    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        return await _context.Products
            .FirstOrDefaultAsync(p => p.Id == productId);
    }

    public async Task<Product> CreateProductAsync(ProductCreationDto productCreationDto)
    {
        ArgumentNullException.ThrowIfNull(productCreationDto);

        // Convert DTO to Product
        var product = new Product()
        {
            Name = productCreationDto.Name,
            Description = productCreationDto.Description,
            Price = productCreationDto.Price,
            Stock = productCreationDto.Stock,
            Category = productCreationDto.Category,
            ImageUrl = productCreationDto.ImageUrl,
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow
        };
        try
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine($"Error creating product: {ex.Message}");
            throw;
        }
    }

    public async Task<Product> UpdateProductAsync(int id, Product product)
    {
        ArgumentNullException.ThrowIfNull(product);

        var productToUpdate = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id) ??
            throw new InvalidOperationException($"Product with ID {id} not found.");

        // Update only the necessary fields
        productToUpdate.Name = product.Name;
        productToUpdate.Description = product.Description;
        productToUpdate.Price = product.Price;
        productToUpdate.Stock = product.Stock;
        productToUpdate.Category = product.Category;
        productToUpdate.ImageUrl = product.ImageUrl;
        productToUpdate.LastUpdatedAt = DateTime.UtcNow;

        try
        {
            _context.Products.Update(productToUpdate);
            await _context.SaveChangesAsync();
            return product;
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine($"Error updating product: {ex.Message}");
            throw;
        }
    }

    public async Task<bool> DeleteProductAsync(int productId)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == productId);

        if (product is null) return false;

        try
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting product with id {productId}: {ex.Message}");
            throw;
        }

    }
}
