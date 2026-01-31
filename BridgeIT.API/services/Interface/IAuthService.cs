using BridgeITAPIs.DTOs.AuthDTOs;

namespace BridgeITAPIs.Auth;

public interface IAuthService
{
    Task<string> AuthenticateAsync(UserDTO userDTO);
}
