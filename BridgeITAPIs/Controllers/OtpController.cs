using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BridgeITAPIs.DTOs.OtpDTOs;

namespace BridgeITAPIs.Controllers;

[Route("api/otp")]
[ApiController]
public class OtpController : ControllerBase
{
    private readonly BridgeItContext _context;
    private readonly MailService _mailService;

    public OtpController(BridgeItContext context, MailService mailService)
    {
        _context = context;
        _mailService = mailService;
    }

    [HttpPost("generate-otp")]
    public async Task<IActionResult> GenerateOtp([FromBody] string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return BadRequest("Email is required.");
        }

        var otp = new Otp
        {
            email = email,
            otp = new Random().Next(100000, 999999),
            created_at = DateTime.UtcNow,
        };

        _context.Set<Otp>().Add(otp);
        await _context.SaveChangesAsync();

        return Ok(otp);
    }

    [HttpPost("verify-otp")]
    public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpDTO otp)
    {
        var otpData = await _context
            .Set<Otp>()
            .FirstOrDefaultAsync(o => o.email == otp.email && o.otp == otp.otp);

        if (otp == null)
        {
            return BadRequest("Otp data is null.");
        }

        if (otp.otp != otpData.otp)
        {
            return BadRequest("Invalid OTP.");
        }

        if (otp.email != otpData.email)
        {
            return BadRequest("Invalid OTP.");
        }

        if (otpData == null)
        {
            return BadRequest("Invalid OTP.");
        }

        if (DateTime.UtcNow.Subtract(otpData.created_at).TotalMinutes > 5)
        {
            return BadRequest("OTP expired.");
        }

        return Ok("OTP verified successfully.");
    }

    [HttpPatch("regenerate-otp")]
    public async Task<IActionResult> ReGenerateOtp([FromBody] string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return BadRequest("Email is required.");
        }

        var otpData = await _context
            .Set<Otp>()
            .FirstOrDefaultAsync(o => o.email == email);

        if (otpData == null)
        {
            return BadRequest("Otp data not found/email doesnot exits.");
        }

        int new_otp = new Random().Next(100000, 999999);

        otpData.otp = new_otp;

        DateTime time_now = DateTime.UtcNow;

        otpData.created_at = time_now;

        await _context.SaveChangesAsync();
        return Ok(new_otp);
    }

    [HttpPost("send-otp")]
    public async Task<IActionResult> SendOtp([FromBody] string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return BadRequest("Email is required.");
        }

        var otpData = await _context
            .Set<Otp>()
            .FirstOrDefaultAsync(o => o.email == email);

        if (otpData == null)
        {
            return BadRequest("Otp data not found/email doesnot exits.");
        }

        await _mailService.SendOtpMail(email, otpData.otp);

        return Ok("OTP sent successfully.");
    }
}
