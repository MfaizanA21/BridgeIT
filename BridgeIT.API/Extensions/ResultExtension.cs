using BridgeIT.Application.Common.Result;
using Microsoft.AspNetCore.Mvc;

namespace BridgeIT.API.Extension;

public static class ResultExtension
{
    /// <summary>
    /// Converts Result<T> to appropriate IActionResult response.
    /// </summary>
    public static IActionResult ToActionResult<T>(
        this Result<T> result,
        ControllerBase controller)
    {
        if (result.IsSuccess)
        {
            if (result.Value is null)
                return controller.NoContent();

            return controller.Ok(result.Value);
        }

        var statusCode = result.StatusCode ?? 500;
        var message = result.ErrorMessage ?? "An unexpected error occurred.";

        return statusCode switch
        {
            400 => controller.BadRequest(message),
            404 => controller.NotFound(message),
            409 => controller.Conflict(message),
            _ => controller.StatusCode(statusCode, message)
        };
    }
}