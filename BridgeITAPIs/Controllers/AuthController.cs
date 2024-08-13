using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BridgeITAPIs.Auth;
namespace BridgeITAPIs.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
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
}
