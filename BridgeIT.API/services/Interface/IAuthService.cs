using BridgeIT.API.DTOs.AuthDTOs;

namespace BridgeIT.API.Auth;

public interface IAuthService
{
    Task<string> AuthenticateAsync(UserDTO userDTO);
}
