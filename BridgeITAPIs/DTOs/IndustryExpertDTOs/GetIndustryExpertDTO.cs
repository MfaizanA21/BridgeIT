using Microsoft.OpenApi.Models;

namespace BridgeITAPIs.DTOs.IndustryExpertDTOs;

public class GetIndustryExpertDTO
{
    public Guid? UserId { get; set; }
    public Guid IndExptId { get; set; }
    public Guid? CompanyId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public byte[] ImageData { get; set; } = Array.Empty<byte>();
    public string Description { get; set; } = string.Empty;
    public string Contact { get; set; } = string.Empty;
    public string Post { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}
