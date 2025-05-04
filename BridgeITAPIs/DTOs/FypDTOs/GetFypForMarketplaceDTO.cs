namespace BridgeITAPIs.DTOs.FypDTOs;

public class GetFypForMarketplaceDTO
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public int? Members { get; set; }
    public string? Description { get; set; }
    public string? Technology { get; set; }
    public string? FypId { get; set; }
    public string? FacultyName { get; set; }
}