
namespace BridgeITAPIs.DTOs.ProjectProgressDTOs;

public class GetProjectProgressDTO
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string Task { get; set; } = String.Empty;
    public string TaskStatus { get; set; } = String.Empty;
}