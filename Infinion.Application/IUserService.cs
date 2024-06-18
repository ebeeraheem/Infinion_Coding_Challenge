using Infinion.Domain;
using Microsoft.AspNetCore.Identity;

namespace Infinion.Application;
public interface IUserService
{
    Task<IdentityResult> ConfirmEmailAsync(string token, string email);
    Task<string?> LoginUserAsync(LoginModel model);
    Task<IdentityResult> RegisterUserAsync(RegisterModel model);
}