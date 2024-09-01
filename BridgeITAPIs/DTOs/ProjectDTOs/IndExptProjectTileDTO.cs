namespace BridgeITAPIs.DTOs.ProjectDTOs;

public class IndExptProjectTileDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
    public Guid? IndExpertId { get; set; }
}
