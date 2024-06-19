using Infinion.Domain.Entities;

namespace Infinion.Application.Services.Interfaces;
public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int productId);
    Task<Product> CreateProductAsync(Product product);
    Task<Product> UpdateProductAsync(int id, Product product);
    Task<bool> DeleteProductAsync(int productId);
}