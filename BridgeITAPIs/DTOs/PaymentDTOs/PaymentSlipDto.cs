namespace BridgeITAPIs.DTOs.PaymentDTOs;

public class PaymentSlipDto
{
    public string projecteeName { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public string ProjectTitle { get; set; } = string.Empty;
    public DateTime PaymentDate { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public string PaymentStatus { get; set; } = string.Empty;
    public string PaymentAmount { get; set; } = string.Empty;
    public string TransactionId { get; set; } = string.Empty;
}