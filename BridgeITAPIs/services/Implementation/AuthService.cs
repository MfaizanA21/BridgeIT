using Microsoft.EntityFrameworkCore;
using BridgeITAPIs.Helper;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using BridgeITAPIs.DTOs.AuthDTOs;


namespace BridgeITAPIs.services.Implementation;

public class AuthService: IAuthService
{
    private readonly BridgeItContext _dbContext;
    private readonly JwtSettings _jwtSettings;

    public AuthService(BridgeItContext dbContext, JwtSettings jwtSettings)
    {
        _dbContext = dbContext;
        _jwtSettings = jwtSettings;
    }

    public async Task<string> AuthenticateAsync(UserDTO userDTO)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Email == userDTO.Email);

        if (user == null || !PasswordHelper.VerifyPassword(userDTO.Password, user.Hash, user.Salt))
        {
            throw new UnauthorizedAccessException("Invalid Credentials");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            }),

            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
