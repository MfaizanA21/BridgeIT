namespace BridgeIT.API.DTOs.ProjectProposalDTOs
{
    public class GetAllProposalDTO
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid StudentId { get; set; }
        public Guid? ExpertId { get; set; }
        public string Proposal { get; set; } = String.Empty;
        public string Status { get; set; } = string.Empty;
        public string ProjectTitle { get; set; } = string.Empty;
        public string ProjectDescription { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public List<string> skills { get; set; } = new List<string>();
        public string university { get; set; } = string.Empty;
        public string department { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        
    }
}
