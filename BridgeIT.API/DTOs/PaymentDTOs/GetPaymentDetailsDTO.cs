namespace BridgeITAPIs.DTOs.PaymentDTOs;

public class GetPaymentDetailsDTO
{
    public Guid Id { get; set; }
    public DateTime PaidAt { get; set; }
    public Guid ProjectId { get; set; }
    public Guid StudentId { get; set; }
    public Guid ProjectOwnerId { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public string ProjectOwnerName { get; set; } = string.Empty;
    public string StudentName { get; set; } = string.Empty;
    public string StudentEmail { get; set; } = string.Empty;
    public string StudentPaymentAccountId { get; set; } = string.Empty;
    public Byte[]? Receipt { get; set; }
}

//913864791722-u2t02qrn3pf8restkgt6e56k1c4civi8.apps.googleusercontent.com CLIENT ID

//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 CLIENT SECRET