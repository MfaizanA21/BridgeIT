namespace BridgeITAPIs.DTOs.ProjectProposalDTOs
{
    public class GetProposalsForStudentsDTO
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid ProjectId { get; set; }
        public Guid? ExpertId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public byte[]? Proposal { get; set; }
        public string Status { get; set; } = string.Empty;
        public string ProjectTitle { get; set; } = string.Empty;
        public string ProjectDescription { get; set; } = string.Empty;
    }
}
