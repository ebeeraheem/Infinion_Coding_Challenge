using Infinion.Application.Services.Interfaces;
using Infinion.Domain.Entities;
using Infinion.Domain.Models;
using Infinion.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace Infinion.Application.Services;
public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _config;
    private readonly ApplicationDbContext _context;

    public UserService(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IEmailService emailService,
        IConfiguration config,
        ApplicationDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailService = emailService;
        _config = config;
        _context = context;
    }

    public async Task<IdentityResult> RegisterUserAsync(RegisterModel model)
    {
        using var transaction = await _context.Database
            .BeginTransactionAsync();
        try
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
                await _emailService.SendEmailAsync(
                    user.Email,
                    "Email Confirmation",
                    $"Please confirm your email by clicking this link: {confirmationLink}");
            }

            // Commit transaction
            await transaction.CommitAsync();

            return result;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<string?> LoginUserAsync(LoginModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user is null)
            return null;

        var result = await _signInManager.PasswordSignInAsync(
            model.Email, model.Password, false, false);

        if (!result.Succeeded)
            return null;

        if (!await _userManager.IsEmailConfirmedAsync(user))
            return null;

            var authClaims = new List<Claim>
        {
            new (ClaimTypes.Name, user.UserName!), // UserName is not null here
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var authSigningKey = new SymmetricSecurityKey(
            // Key is not null
            Encoding.UTF8.GetBytes(_config.GetValue<string>("Jwt:Key")!));

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

        // Properly encode query parameters
        var encodedToken = HttpUtility.UrlEncode(token);
        var encodedEmail = HttpUtility.UrlEncode(email);

        var confirmationLink = $"{baseUrl}/api/Auth/ConfirmEmail?token={encodedToken}&email={encodedEmail}";

        return confirmationLink;
    }
}
