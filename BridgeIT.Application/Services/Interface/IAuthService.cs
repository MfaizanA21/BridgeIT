using BridgeIT.API.DTOs.AuthDTOs;

namespace BridgeIT.Application.Interface;

public interface IAuthService
{
    Task<string> AuthenticateAsync(UserDTO userDTO);
}
