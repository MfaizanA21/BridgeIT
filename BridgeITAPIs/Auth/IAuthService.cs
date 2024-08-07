using BridgeITAPIs.DTOs;

namespace BridgeITAPIs.Auth;

public interface IAuthService
{
    Task<string> AuthenticateAsync(UserDTO userDTO);
}
