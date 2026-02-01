using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BridgeIT.API.services.Implementation;

public class UserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public ClaimsPrincipal GetCurrentUser()
    {
        return _httpContextAccessor.HttpContext.User;
    }

    public string GetCurrentUserId()
    {
        return GetCurrentUser()?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }

    public string GetCurrentRole()
    {
        return GetCurrentUser()?.FindFirst(ClaimTypes.Role)?.Value;
    }
}
