using System.ComponentModel.DataAnnotations;

namespace BridgeITAPIs.DTOs.PaymentDTOs;

public class FypPaymentDto
{
    [Required]
    public int price { get; set; }
    
    [Required]
    public Guid IndExpertId { get; set; }
}