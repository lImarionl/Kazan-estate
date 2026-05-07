using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using KazanRealEstate.Api.Repositories.Tables;
using KazanRealEstate.Api.Services;

namespace KazanRealEstate.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PropertyController : BaseController
{
    private readonly IPropertyService _propertyService;

    public PropertyController(IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    [HttpGet("complexes")]
    public async Task<IActionResult> GetComplexes()
    {
        return HandleResult(await _propertyService.GetComplexesAsync());
    }

    [HttpGet("developers")]
    public async Task<IActionResult> GetDevelopers()
    {
        return HandleResult(await _propertyService.GetDevelopersAsync());
    }
}
