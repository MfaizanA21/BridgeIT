using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BridgeITAPIs.Helper;

namespace BridgeITAPIs.Controllers;

[Route("api/edit-user-profile")]
[ApiController]
public class EditUserProfileController : ControllerBase
{
    private readonly BridgeItContext _dbContext;

    public EditUserProfileController(BridgeItContext dbContext)
    {
        _dbContext = dbContext;
    }


    [HttpPut("set-profile-image/{Id}")]
    public async Task<IActionResult> SetProfileImage(Guid Id, string base64ImageData)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == Id);

        if (user == null)
        {
            return NotFound("Student not found.");
        }

        if (user != null)
        {
            try
            {
                if (!string.IsNullOrEmpty(base64ImageData))
                {
                    user.ImageData = Convert.FromBase64String(base64ImageData);
                }
            }
            catch (FormatException ex)
            {
                return BadRequest("Invaid base64 string");
            }
        }

        await _dbContext.SaveChangesAsync();

        return Ok("Profile image uploaded successfully.");
    }

    [HttpPut("change-password/{Id}")]
    public async Task<IActionResult> ChangeUserPassword(Guid Id, string password)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == Id);

        if (user == null)
        {
            return NotFound("User not found.");
        }

        var (passwordHash, passwordSalt) = PasswordHelper.HashPassword(password);

        if (user != null)
        {
            user.Hash = passwordHash;
            user.Salt = passwordSalt;
        }

        await _dbContext.SaveChangesAsync();
        return Ok("Password Changed Successfully");
    }

    [HttpGet("confirm-current-password/{Id}")]
    public async Task<IActionResult> GetCurrentPassword(Guid Id, string previousPassword)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == Id);

        if (user == null)
        {
            return NotFound("User not found.");
        }

        if (!PasswordHelper.VerifyPassword(previousPassword, user.Hash, user.Salt))
        {
            return BadRequest("Invalid Password");
        }

        return Ok("Password Confirmed");
    }
}
