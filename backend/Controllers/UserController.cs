using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using KazanRealEstate.Api.DTOs;
using KazanRealEstate.Api.Services;

namespace KazanRealEstate.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : BaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        return HandleResult(await _userService.GetProfileAsync());
    }

    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto model)
    {
        return HandleResult(await _userService.UpdateProfileAsync(model));
    }
}
