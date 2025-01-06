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

    public async Task SendInterestRejectionMailToStudent(string to_mail, string ideaName, string facultyName)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress("BridgeIT", _configuration["SmtpSettings:SenderEmail"]));
        email.To.Add(MailboxAddress.Parse(to_mail));
        email.Subject = "BridgeIT Idea Update";
        
        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = $@"
                <div style='font-family: Arial, sans-serif; padding: 20px; color: #333;'>
                    <h2 style='color: #0066cc;'>BridgeIT: Idea Request Update</h2>
                    <p>Hello,</p>
                    <p><strong>{facultyName}</strong> is not interested in your request to work on <strong>{ideaName}</strong> idea.</p>
                    <p>Sad to you see go. </p>
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

    
    public async Task SendInterestAcceptanceMailToStudent(string to_mail, string ideaName, string facultyName,
        string time)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress("BridgeIT", _configuration["SmtpSettings:SenderEmail"]));
        email.To.Add(MailboxAddress.Parse(to_mail));
        email.Subject = "BridgeIT Idea Meeting Notification";
        
        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = $@"
                <div style='font-family: Arial, sans-serif; padding: 20px; color: #333;'>
                    <h2 style='color: #0066cc;'>BridgeIT: Meeting Scheduled for Idea Discussion</h2>
                    <p>Hello,</p>
                    <p><strong>{facultyName}</strong> have scheduled a meeting at/on <strong>{time}</strong> for discussion of <strong>{ideaName}</strong> idea.</p>
                    <p>Good luck for the Meeting. </p>
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

    public async Task SendStudentInterestedForIdeaMailToFaculty(string to_mail, string ideaName, string stdName)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress("BridgeIT", _configuration["SmtpSettings:SenderEmail"]));
        email.To.Add(MailboxAddress.Parse(to_mail));
        email.Subject = "BridgeIT Student Interested in Idea";
        
        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = $@"
                <div style='font-family: Arial, sans-serif; padding: 20px; color: #333;'>
                    <h2 style='color: #0066cc;'>BridgeIT: Student Interested in Idea</h2>
                    <p>Hello,</p>
                    <p>Your Idea <strong>{ideaName}</strong> is in interest of <strong>{stdName}</strong>.</p>
                    <p>Check your Notification tab from BridgeIT to schedule a meeting. </p>
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
                    <h2 style='color: #0066cc;'>BridgeIT: Proposal Status</h2>
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
                    <h2 style='color: #0066cc;'>BridgeIT: Project Proposal</h2>
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
