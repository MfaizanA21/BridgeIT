namespace BridgeITAPIs.DTOs.PaymentDTOs;

public class CreatePaymentIntentDto
{
    public double Amount { get; set; }
    public string StudentPaymentAccountId { get; set; } = string.Empty;
    public string ProjectId { get; set; } = String.Empty;
}