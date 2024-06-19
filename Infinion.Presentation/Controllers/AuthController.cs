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

    /// <summary>
    /// Registers a new user in the system.
    /// </summary>
    /// <param name="model">The user registration information including email, password, first name, and last name.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> that returns:
    /// <list type="bullet">
    /// <item><description>200 OK if registration is successful.</description></item>
    /// <item><description>400 Bad Request if the registration information is invalid.</description></item>
    /// <item><description>500 Internal Server Error if there is an issue with the server.</description></item>
    /// </list>
    /// </returns>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _userService.RegisterUserAsync(model);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Registration successful, please check your email to confirm your account.");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occured. User registration failed.");
        }
    }

    /// <summary>
    /// Logs in an existing user and generates a JWT token.
    /// </summary>
    /// <param name="model">The login information including email and password.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> that returns:
    /// <list type="bullet">
    /// <item><description>200 OK with a JWT token if login is successful.</description></item>
    /// <item><description>400 Bad Request if the login information is invalid.</description></item>
    /// <item><description>401 Unauthorized if the user credentials are incorrect.</description></item>
    /// </list>
    /// </returns>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var loginResult = await _userService.LoginUserAsync(model);

        if (!loginResult.Succeeded)
        {
            return Unauthorized(loginResult.ErrorMessage);
        }

        return Ok(new { Token = loginResult .Token});
    }

    /// <summary>
    /// Confirms the email of a newly registered user.
    /// </summary>
    /// <param name="token">The email confirmation token sent to the user.</param>
    /// <param name="email">The email address of the user to be validated.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> that returns:
    /// <list type="bullet">
    /// <item><description>200 OK if email confirmation is successful.</description></item>
    /// <item><description>400 Bad Request if the token or email is invalid.</description></item>
    /// <item><description>500 Internal Server Error if there is an issue with the server.</description></item>
    /// </list>
    /// </returns>
    [HttpGet("confirmemail")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
