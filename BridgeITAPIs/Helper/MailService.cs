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

    public async Task ProjectProposalStatusMail(string to_mail, string projectName, string status)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress("BridgeIT", _configuration["SmtpSettings:SenderEmail"]));
        email.To.Add(MailboxAddress.Parse(to_mail));
        email.Subject = "BridgeIT Proposal Submission";
        
        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = $@"
                <div style='font-family: Arial, sans-serif; padding: 20px; color: #333;'>
                    <h2 style='color: #0066cc;'>BridgeIT: FYP Approval</h2>
                    <p>Hello,</p>
                    <p>Your Proposal for <strong>{projectName}</strong> project has been <strong>{status}</strong>.</p>
                    <p>Don't stop just now, Keep grinding </p>
                    <br></br>
                    <p>Regards,<br/>BridgeIT Team</p>
                </div>"
        };
        email.Body = bodyBuilder.ToMessageBody();
        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_configuration["SmtpSettings:Server"], int.Parse(_configuration["SmtpSettings:Port"]), MailKit.Security.SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_configuration["SmtpSettings:Username"], _configuration["SmtpSettings:Password"]);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
    
    public async Task SendProjectProposalMail(string to_mail, string projectName)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress("BridgeIT", _configuration["SmtpSettings:SenderEmail"]));
        email.To.Add(MailboxAddress.Parse(to_mail));
        email.Subject = "BridgeIT Proposal Submission";
        
        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = $@"
                <div style='font-family: Arial, sans-serif; padding: 20px; color: #333;'>
                    <h2 style='color: #0066cc;'>BridgeIT: FYP Approval</h2>
                    <p>Hello,</p>
                    <p>Your Proposal for <strong>{projectName}</strong> project has been sent.</p>
                    <p>Good luck in getting the project. </p>
                    <br></br>
                    <p>Regards,<br/>BridgeIT Team</p>
                </div>"
        };
        email.Body = bodyBuilder.ToMessageBody();
        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_configuration["SmtpSettings:Server"], int.Parse(_configuration["SmtpSettings:Port"]), MailKit.Security.SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_configuration["SmtpSettings:Username"], _configuration["SmtpSettings:Password"]);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
    
    public async Task SendFypMail(string to_mail, string fypName, string result)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress("BridgeIT", _configuration["SmtpSettings:SenderEmail"]));
        email.To.Add(MailboxAddress.Parse(to_mail));
        email.Subject = "BridgeIT FYP Approval";

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = $@"
                <div style='font-family: Arial, sans-serif; padding: 20px; color: #333;'>
                    <h2 style='color: #0066cc;'>BridgeIT: FYP Approval</h2>
                    <p>Hello,</p>
                    <p>Your FYP <strong>{fypName}</strong> has been <strong>{result}</strong> by the university admin.</p>
                    <p>For Further details please contact your university admin. </p>
                    <br></br>
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
