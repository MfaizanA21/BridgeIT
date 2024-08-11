namespace BridgeITAPIs.DTOs;

public class ProjectTileDTO
{
    public Guid Id { get; set; }
    public string Title{ get; set; } = string.Empty;
    public string Description { get; set;} = string.Empty;
    public string Stack { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public Guid? StudentId { get; set; }
    public Guid? IndExpertId { get; set; }
    public string studentName { get; set; } = string.Empty;
    public string IndExpertName { get; set; } = string.Empty;


}
