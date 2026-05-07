using Microsoft.AspNetCore.Mvc;
using KazanRealEstate.Api.Common;

namespace KazanRealEstate.Api.Controllers;

public class BaseController : ControllerBase
{
    protected IActionResult HandleResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
            return result.Data != null ? Ok(result) : Ok(result);
        
        return StatusCode(result.StatusCode, result);
    }

    protected IActionResult HandleResult(Result result)
    {
        if (result.IsSuccess)
            return Ok(result);
        
        return StatusCode(result.StatusCode, result);
    }
}
