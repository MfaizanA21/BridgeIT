namespace BridgeITAPIs.DTOs.ProjectDTOs;

public class IndExptProjectTileDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? EndDate { get; set; } = string.Empty;
    public string CurrentStatus { get; set; } = string.Empty;
    public string? Name {  get; set; } = string.Empty;
    public int Budget { get; set; }
    public Guid? IndExpertId { get; set; }
    public Guid? StudentId { get; set; } = null;
}
