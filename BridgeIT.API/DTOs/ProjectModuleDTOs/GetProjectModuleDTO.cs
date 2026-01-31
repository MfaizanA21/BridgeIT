namespace BridgeITAPIs.DTOs.ProjectModuleDTOs;

public class GetProjectModuleDTO
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool? Status { get; set; }
    public string ProjectName { get; set; } = string.Empty;
}