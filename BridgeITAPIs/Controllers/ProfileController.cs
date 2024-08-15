using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BridgeITAPIs.Controllers;

[Route("api/user-profile")]
[ApiController]
[Authorize]
public class ProfileController : ControllerBase
{
    private readonly UserService _userService;

    public ProfileController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet("authorized-user-info")]
    public IActionResult GetProfile()
    {
        var userId = _userService.GetCurrentUserId();
        var role = _userService.GetCurrentRole();


        return Ok(new { UserId = userId, Role = role });
    }
}
