// 21/12/2025 - 18:02:53
// DANGTHUY

using System.Security.Claims;
using KnowledgeHub.Models.Base;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeHub.Api.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected IActionResult OkResponse<T>(T data, string? message = null)
    {
        return Ok(BaseResponse<T>.Ok(data, message));
    }

    protected IActionResult FailResponse(
        string code,
        string message,
        int statusCode = StatusCodes.Status400BadRequest)
    {
        return StatusCode(
            statusCode,
            BaseResponse<object>.Fail(code, message));
    }

    protected Guid? CurrentUserId
    {
        get
        {
            var sub = User?.FindFirstValue(ClaimTypes.NameIdentifier)
                      ?? User?.FindFirstValue("sub");

            return Guid.TryParse(sub, out var id) ? id : null;
        }
    }
}