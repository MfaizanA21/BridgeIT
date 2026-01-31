using Microsoft.AspNetCore.Mvc;
using BridgeITAPIs.DTOs.AuthDTOs;
using BridgeITAPIs.services.Implementation;
namespace BridgeITAPIs.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    private readonly UserService _userService;

    public AuthController(IAuthService authService, UserService userService)
    {
        _authService = authService;
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserDTO userDTO)
    {
        try
        {
            var token = await _authService.AuthenticateAsync(userDTO);
            return Ok(new { Token = token });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
    }

    [HttpGet("authorized-user-info")]
    public IActionResult GetProfile()
    {
        var userId = _userService.GetCurrentUserId();
        var role = _userService.GetCurrentRole();


        return Ok(new { UserId = userId, Role = role });
    }
    
}
