using Microsoft.AspNetCore.Mvc;
using KazanRealEstate.Api.Services;
using KazanRealEstate.Api.DTOs;

namespace KazanRealEstate.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : BaseController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto model)
    {
        return HandleResult(await _authService.RegisterAsync(model.Username, model.Password, model.Email));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto model)
    {
        return HandleResult(await _authService.LoginAsync(model.Username, model.Password));
    }
}
