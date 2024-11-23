namespace BridgeITAPIs.Models
{
    public class ProjectProposal
    {
        public Guid Id {  get; set; }
        
        public byte[] Proposal { get; set; } = Array.Empty<byte>();

        public string Status { get; set; } = string.Empty;

        public Guid ProjectId { get; set; }

        public Guid StudentId { get; set; }

        public virtual Project? Project { get; set; }

        public virtual Student? Student { get; set; }

    }
}
