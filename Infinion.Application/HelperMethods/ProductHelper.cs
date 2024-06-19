using Infinion.Domain.Entities;

namespace Infinion.Application.HelperMethods;
public static class ProductHelper
{
    public static IEnumerable<Product> FilterProducts(
        this IEnumerable<Product> products,
            string? category = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            bool? inStock = null,
            string? name = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string? sortBy = null,
            string? sortOrder = "asc",
            int page = 1,
            int pageSize = 10)
    {


        return products;
    }
}
