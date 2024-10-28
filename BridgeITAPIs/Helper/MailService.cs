using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace BridgeITAPIs.Helper;

public class MailService
{
    private readonly IConfiguration _configuration;

    public MailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendOtpMail(string to_mail, int otp)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress("BridgeIT", _configuration["SmtpSettings:SenderEmail"]));
        email.To.Add(MailboxAddress.Parse(to_mail));
        email.Subject = "BridgeIT OTP Verification";

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = $@"
                <div style='font-family: Arial, sans-serif; padding: 20px; color: #333;'>
                    <h2 style='color: #0066cc;'>BridgeIT: Email Verification</h2>
                    <p>Hello,</p>
                    <p>Your OTP code for email verification is:</p>
                    <h3 style='background: #f2f2f2; padding: 10px; border-radius: 5px; display: inline-block;'>{otp}</h3>
                    <p>Please enter this code to complete your email verification. your otp will expire in 5 mintues.</p>
                    <p>Thank you,<br/>BridgeIT Team</p>
                </div>"
        };
        email.Body = bodyBuilder.ToMessageBody();

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_configuration["SmtpSettings:Server"], int.Parse(_configuration["SmtpSettings:Port"]), MailKit.Security.SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_configuration["SmtpSettings:Username"], _configuration["SmtpSettings:Password"]);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }

}
