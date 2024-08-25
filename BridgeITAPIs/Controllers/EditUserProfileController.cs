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
    public async Task<IActionResult> SetProfileImage(Guid Id, [FromBody] string base64ImageData)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == Id);

        if (user == null)
        {
            return NotFound("User not found.");
        }

        try
        {
            if (!string.IsNullOrEmpty(base64ImageData))
            {
                user.ImageData = Convert.FromBase64String(base64ImageData);
                await _dbContext.SaveChangesAsync();
                return Ok("Profile image uploaded successfully.");
            }
            else
            {
                return BadRequest("Image data is empty.");
            }
        }
        catch (FormatException ex)
        {
            return BadRequest("Invalid base64 string.");
        }
    }

    [HttpPut("change-password/{Id}")]
    public async Task<IActionResult> ChangeUserPassword(Guid Id, [FromBody] string password)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == Id);

        if (user == null)
        {
            return NotFound("User not found.");
        }

        var (passwordHash, passwordSalt) = PasswordHelper.HashPassword(password);

        user.Hash = passwordHash;
        user.Salt = passwordSalt;

        await _dbContext.SaveChangesAsync();
        return Ok("Password changed successfully.");
    }

    [HttpPost("confirm-current-password/{Id}")]
    public async Task<IActionResult> GetCurrentPassword(Guid Id, [FromBody] string previousPassword)
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

        return Ok("Password confirmed.");
    }

    [HttpPut("update-user-description/{Id}")]
    public async Task<IActionResult> UpdateUserDescription(Guid Id, [FromBody] string description)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == Id);

        if (user == null)
        {
            return NotFound("User not found.");
        }

        user.description = description;
        await _dbContext.SaveChangesAsync();
        return Ok("Description updated successfully.");
    }
}
