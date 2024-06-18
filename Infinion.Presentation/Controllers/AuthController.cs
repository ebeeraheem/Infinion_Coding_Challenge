using Infinion.Application;
using Infinion.Domain;
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
}
