namespace BridgeIT.Domain.Models;

public class Otp
{
    public string email { get; set; }
    public int otp { get; set; }
    public DateTime created_at { get; set; }
}
