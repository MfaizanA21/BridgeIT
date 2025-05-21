using MailKit.Net.Smtp;
using MimeKit;

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
        string time, string place)
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
                    <p><strong>{facultyName}</strong> have scheduled a meeting at/on <strong>{time}</strong> at <strong>{place}</strong> for discussion of <strong>{ideaName}</strong> idea.</p>
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

    public async Task SendFypApprovalStatusMail(string to_mail, string Name, string fypTitle, bool isApproved)
    {
        var statusText = isApproved ? "approved" : "rejected";
        var suggestionText = isApproved 
            ? "Congratulations! You can now proceed further with the FYP. Arrange a meeting with University admin or concerned student to discuss the next steps." 
            : "For some internal reasons the university would not like to move further with your request.";

        var email = new MimeMessage();
        email.From.Add(new MailboxAddress("BridgeIT", _configuration["SmtpSettings:SenderEmail"]));
        email.To.Add(MailboxAddress.Parse(to_mail));
        email.Subject = $"BridgeIT FYP {statusText.First().ToString().ToUpper() + statusText.Substring(1)}";

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = $@"
            <div style='font-family: Arial, sans-serif; padding: 20px; color: #333;'>
                <h2 style='color: #0066cc;'>BridgeIT: FYP {statusText.First().ToString().ToUpper() + statusText.Substring(1)}</h2>
                <p>Hello {Name},</p>
                <p>Your Interested FYP titled <strong>{fypTitle}</strong>'s request has been <strong>{statusText}</strong> by the university admin.</p>
                <p>{suggestionText}</p>
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

    public async Task NotifyStudentFypInterest(string to_mail, string studentName, string interestedPersonName, string interestedPersonMail, string fypTitle)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress("BridgeIT", _configuration["SmtpSettings:SenderEmail"]));
        email.To.Add(MailboxAddress.Parse(to_mail));
        email.Subject = "BridgeIT: Interest Shown in Your FYP";

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = $@"
        <div style='font-family: Arial, sans-serif; padding: 20px; color: #333;'>
            <h2 style='color: #0066cc;'>BridgeIT: FYP Interest Notification</h2>
            <p>Hello {studentName},</p>
            <p><strong>{interestedPersonName}</strong> has shown interest in your FYP titled <strong>{fypTitle}</strong>.</p>
            <p>Please contact them at <strong>{interestedPersonMail}</strong> to arrange a meeting and discuss further collaboration or guidance.</p>
            <p>Take this opportunity to align on expectations and next steps.</p>
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

    public async Task NotifyStudentAboutMeetingTime(string to_mail, string interestedPersonName, string fypTitle, DateTime time)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress("BridgeIT", _configuration["SmtpSettings:SenderEmail"]));
        email.To.Add(MailboxAddress.Parse(to_mail));
        email.Subject = "BridgeIT: Meeting Scheduled";

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = $@"
        <div style='font-family: Arial, sans-serif; padding: 20px; color: #333;'>
            <h2 style='color: #0066cc;'>BridgeIT: Meeting Scheduled</h2>
            <p>Hello,</p>
            <p>Your FYP titled <strong>{fypTitle}</strong> has a meeting scheduled with <strong>{interestedPersonName}</strong> at/on <strong>{time}</strong>.</p>
            <p>Please be prepared for the discussion.</p>
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
}
