using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using KazanRealEstate.Api.Services;

namespace KazanRealEstate.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FavoriteController : BaseController
{
    private readonly IFavoriteService _favoriteService;

    public FavoriteController(IFavoriteService favoriteService)
    {
        _favoriteService = favoriteService;
    }

    [HttpGet]
    public async Task<IActionResult> GetMyFavorites()
    {
        return HandleResult(await _favoriteService.GetMyFavoritesAsync());
    }

    [HttpPost("toggle/{complexId}")]
    public async Task<IActionResult> ToggleFavorite(int complexId)
    {
        return HandleResult(await _favoriteService.ToggleFavoriteAsync(complexId));
    }

    [HttpGet("compare")]
    public async Task<IActionResult> CompareWithAi()
    {
        return HandleResult(await _favoriteService.CompareWithAiAsync());
    }
}
