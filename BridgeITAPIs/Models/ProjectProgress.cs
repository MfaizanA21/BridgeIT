using BridgeITAPIs.Enums;

namespace BridgeITAPIs.Models;

public partial class ProjectProgress
{
    public Guid Id { get; set; }

    public Guid ProjectId { get; set; }
    
    required public string Task { get; set; }
    
    public string TaskStatus { get; set; } = ProjectProgressStatus.PENDING.ToString();
    
    public virtual Project? Project { get; set; }
}