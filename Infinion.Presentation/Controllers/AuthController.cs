using Infinion.Application.Services.Interfaces;
using Infinion.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Infinion.Presentation.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }            

        var result = await _userService.RegisterUserAsync(model);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }            

        return Ok("Registration successful, please check your email to confirm your account.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var token = await _userService.LoginUserAsync(model);

        if (token is null)
        {
            return Unauthorized();
        }

        return Ok(new { Token = token });
    }

    [HttpGet("confirmemail")]
    public async Task<IActionResult> ConfirmEmail(string token, string email)
    {
        try
        {
            var result = await _userService.ConfirmEmailAsync(token, email);

            if (result.Succeeded)
            {
                return Ok("Email confirmed successfully.");
            }

            return BadRequest("Email confirmation failed.");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occured");
        }
    }
}
