namespace BridgeITAPIs.Models;

public class InterestedForIdea
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid IdeaId { get; set; }
    public int? Status { get; set; } = null;
    
    public virtual Student? Student { get; set; }
    public virtual Idea? Idea { get; set; }
}