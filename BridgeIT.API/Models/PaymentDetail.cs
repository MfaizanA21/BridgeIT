namespace BridgeITAPIs.Models;

public partial class PaymentDetail
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public DateTime PaidAt { get; set; }
    public required byte[] PaymentSlip { get; set; }
    public virtual Project? Project { get; set; }
}