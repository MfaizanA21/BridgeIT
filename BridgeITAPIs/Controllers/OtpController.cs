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

       // var mails = _context.Set<Otp>().FirstOrDefaultAsync(otp => otp.email == email);

        /*if (mails != null)
        {
            if (DateTime.UtcNow.Subtract(mails.Result.created_at).TotalMinutes < 5)
            {

                return Ok(mails.Result.otp);
            }
            else
            {
                _context.Set<Otp>().Remove(mails.Result);
                await _context.SaveChangesAsync();
            }
        }*/

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
            .FirstOrDefaultAsync(o => o.email == otp.email);

        if (otpData == null)
        {
            return BadRequest("Invalid OTP.");
        }

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


        if (otpData != null && DateTime.UtcNow.Subtract(otpData.created_at).TotalMinutes > 5)
        { 
            _context.Set<Otp>().Remove(otpData);
            await _context.SaveChangesAsync();
            return BadRequest("OTP expired.");
        }
        
        _context.Set<Otp>().Remove(otpData!);
        await _context.SaveChangesAsync();
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

        if (otpData.created_at.AddMinutes(5) > DateTime.UtcNow)
        {
            return BadRequest($"OTP is not expired yet. {otpData.otp}");
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
