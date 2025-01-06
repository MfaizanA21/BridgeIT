using System.ComponentModel.DataAnnotations;

namespace BridgeITAPIs.DTOs.FypDTOs;

public class RegisterFypDTO
{
    [Required]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    public string fyp_id { get; set; } = string.Empty;
    
    [Range(1, int.MaxValue, ErrorMessage = "Members must be greater than 0")]
    public int Members { get; set; }
    
    [Required]
    public string Batch { get; set; } = string.Empty;
    
    [Required]
    public string Technology { get; set; } = string.Empty;
    
    [Required]
    public string Description { get; set; } = string.Empty;
    
    public Guid FacultyId { get; set; } = Guid.Empty;
}