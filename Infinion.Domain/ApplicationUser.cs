using Microsoft.AspNetCore.Identity;

namespace Infinion.Domain;
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
