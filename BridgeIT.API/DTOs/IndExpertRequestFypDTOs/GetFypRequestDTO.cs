namespace BridgeITAPIs.DTOs.IndExpertRequestFypDTOs;

public class GetFypRequestDTO
{
    public Guid Id { get; set; }
    public int? Status { get; set; }
    public Guid[]? StudentIds { get; set; }
    public Guid FypId { get; set; }
    public Guid IndustryExpertId { get; set; }
    public string IndustryExpertName { get; set; } = string.Empty;
    public string FypTitle { get; set; } = string.Empty;
    public string FypDescription { get; set; } = string.Empty;
    public string Fyp_fypId { get; set; } = string.Empty;
}