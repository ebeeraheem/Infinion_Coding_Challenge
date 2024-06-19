using Infinion.Infrastructure.Data;

namespace Infinion.Application.Services;
public class ProductService
{
    private readonly ApplicationDbContext _context;

    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }
}
