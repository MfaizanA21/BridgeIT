namespace BridgeITAPIs.Models
{
    public class ProjectProposal
    {
        public Guid Id {  get; set; }
        
        public string Proposal { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public Guid ProjectId { get; set; }

        public Guid StudentId { get; set; }

        public virtual Project? Project { get; set; }

        public virtual Student? Student { get; set; }

    }
}
