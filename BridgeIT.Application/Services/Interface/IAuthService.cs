using BridgeIT.API.DTOs.AuthDTOs;

namespace BridgeIT.Application.Services.Interface;

public interface IAuthService
{
    Task<string> AuthenticateAsync(UserDTO userDTO);
}
