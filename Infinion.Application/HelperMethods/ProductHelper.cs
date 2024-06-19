using Infinion.Domain.Entities;
using Infinion.Domain.Results;
using Microsoft.EntityFrameworkCore;

namespace Infinion.Application.HelperMethods;
public static class ProductHelper
{
    public static PagedResult<Product> FilterProducts(
        this IEnumerable<Product> products,
            string? category = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            bool? inStock = null,
            string? name = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            int page = 1,
            int pageSize = 10)
    {
        if (!string.IsNullOrWhiteSpace(category))
        {
            products = products.Where(p => p.Category == category);
        }

        if (minPrice.HasValue)
        {
            products = products.Where(p => p.Price >= minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            products = products.Where(p => p.Price <= maxPrice.Value);
        }

        if (inStock.HasValue)
        {
            products = products.Where(
                p => inStock.Value ? p.Stock > 0 : p.Stock == 0);
        }

        if (!string.IsNullOrWhiteSpace(name))
        {
            products = products.Where(p => p.Name.Contains(name));
        }

        if (startDate.HasValue)
        {
            products = products.Where(p => p.CreatedAt >= startDate.Value || p.LastUpdatedAt >= startDate.Value);
        }

        if (endDate.HasValue)
        {
            products = products.Where(p => p.CreatedAt <= endDate.Value || p.LastUpdatedAt <= endDate.Value);
        }

        var totalItems = products.Count();

        var pagedProducts = products
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PagedResult<Product>(totalItems, pagedProducts);
    }
}
