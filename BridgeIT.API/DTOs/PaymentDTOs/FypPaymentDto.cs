using System.ComponentModel.DataAnnotations;

namespace BridgeIT.API.DTOs.PaymentDTOs;

public class FypPaymentDto
{
    [Required]
    public int price { get; set; }
    
    [Required]
    public Guid IndExpertId { get; set; }
}