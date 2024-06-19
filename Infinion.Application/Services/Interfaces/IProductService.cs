using Infinion.Domain.Entities;

namespace Infinion.Application.Services.Interfaces;
public interface IProductService
{
    Task<Product> CreateProductAsync(Product product);
    Task<bool> DeleteProductAsync(int productId);
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int productId);
    Task<Product> UpdateProductAsync(Product product);
}