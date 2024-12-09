using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BridgeITAPIs.Controllers;

[Route("api/forgot-password")]
[ApiController]
public class ForgotPasswordController : Controller
{
    private readonly BridgeItContext _context;
    private readonly MailService _mailService;

    public ForgotPasswordController(BridgeItContext context, MailService mailService)
    {
        _context = context;
        _mailService = mailService;
    }

    [HttpPost("generate-otp")]
    public async Task<IActionResult> GenerateAndSendOtp([FromBody] string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return BadRequest("Enter your email");
        }

        var users = await _context.Set<User>().FirstOrDefaultAsync(u => u.Email == email);
        
        if (users == null)
        {
            return BadRequest("email is not registered");
        }
        int forgototp = new Random().Next(100000, 999999);
        var otp = new ForgotPasswordOtp
        {
            email = email,
            otp = forgototp,
            created_at = DateTime.UtcNow,
        };
        
        _context.Set<ForgotPasswordOtp>().Add(otp);
        await _context.SaveChangesAsync();

        await _mailService.SendOtpMail(email, forgototp);
        
        return Ok("OTP sent successfully");

    }
    
}