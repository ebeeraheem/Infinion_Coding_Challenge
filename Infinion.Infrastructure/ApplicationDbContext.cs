using Infinion.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infinion.Infrastructure;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : 
        base(options) 
    {
    }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
}

