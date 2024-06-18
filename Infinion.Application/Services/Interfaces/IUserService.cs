using Infinion.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Infinion.Application.Services.Interfaces;
public interface IUserService
{
    Task<IdentityResult> ConfirmEmailAsync(string token, string email);
    Task<string?> LoginUserAsync(LoginModel model);
    Task<IdentityResult> RegisterUserAsync(RegisterModel model);
}