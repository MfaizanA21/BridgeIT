namespace BridgeITAPIs.Models;

public class EductionalResource
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? SourceLink { get; set; }
    public Guid FacultyId { get; set; }
    
    public virtual Faculty? Faculty { get; set; }
}