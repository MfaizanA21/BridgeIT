namespace BridgeIT.Application.Abstractions.Service.Interface;

public interface IAuthService
{
    Task<string> AuthenticateAsync(UserDTO userDTO);
}
