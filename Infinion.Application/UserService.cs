using Infinion.Domain.Entities;
using Infinion.Domain.Models;
using Infinion.Infrastructure.HelperMethods;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infinion.Application;
public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _config;

    public UserService(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IConfiguration config)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _config = config;
    }

    public async Task<IdentityResult> RegisterUserAsync(RegisterModel model)
    {
        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName
        };

        // Identity hashes passwords by default
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            // Generate confirmation link
            var confirmationLink = GenerateEmailConfirmationLink(token, user.Email);

            // Send confirmation email
        }

        return result;
    }

    public async Task<string?> LoginUserAsync(LoginModel model)
    {
        var result = await _signInManager.PasswordSignInAsync(
            model.Email, model.Password, false, false);

        if (!result.Succeeded)
            return null;

        var user = await _userManager.FindByEmailAsync(model.Email);
        var authClaims = new List<Claim>
        {
            new (ClaimTypes.Name, user.UserName),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var authSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config.GetValue<string>("Jwt:Key")));

        var signingCredentials = new SigningCredentials(
            authSigningKey, SecurityAlgorithms.HmacSha256);

        // Generate token
        var token = new JwtSecurityToken(
        issuer: _config.GetValue<string>("Jwt:Issuer"),
        audience: _config.GetValue<string>("Jwt:Audience"),
        claims: authClaims,
        notBefore: DateTime.UtcNow,
        expires: DateTime.UtcNow.AddMinutes(3),
        signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<IdentityResult> ConfirmEmailAsync(string token, string email)
    {
        var user = await _userManager.FindByEmailAsync(email) ??
            throw new InvalidOperationException("Invalid email address");

        var result = await _userManager.ConfirmEmailAsync(user, token);
        return result;
    }

    private string GenerateEmailConfirmationLink(string token, string email)
    {
        var baseUrl = _config.GetValue<string>("AppSettings:BaseUrl");

        var confirmationLink = UrlHelper.GenerateConfirmationLink(
            baseUrl!, // baseUrl is not null here
            "Auth", 
            "ConfirmEmail", 
            token, 
            email);

        return confirmationLink;
    }
}
