namespace TodoApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using TodoApi.DTOs;
using TodoApi.Services;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _service;

    public AuthController(AuthService service)
    {
        _service = service;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterRequest registerRequest)
    {
        var result = _service.Register(
            registerRequest.Email,
            registerRequest.Password,
            registerRequest.Name
        );

        if (result.error != null)
            return BadRequest(result.error);

        Response.Cookies.Append(
            "token",
            result.token!,
            new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(7),
            }
        );
        return Ok();
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest loginRequest)
    {
        var result = _service.Login(loginRequest.Email, loginRequest.Password);
        if (result.error != null)
            return BadRequest(result.error);

        Response.Cookies.Append(
            "token",
            result.token!,
            new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(7),
            }
        );
        return Ok();
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("token");
        return Ok();
    }
}
