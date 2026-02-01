namespace BridgeIT.Domain.Models;

public class ForgotPasswordOtp
{
    public int id { get; set; } 
    public string email { get; set; } = string.Empty;
    public int otp { get; set; }
    public DateTime created_at { get; set; }
}