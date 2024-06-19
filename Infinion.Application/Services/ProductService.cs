using Infinion.Application.Services.Interfaces;
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

    public async Task<Product> CreateProductAsync(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);

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

    public async Task<Product> UpdateProductAsync(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);

        try
        {
            _context.Products.Update(product);
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
