namespace BridgeITAPIs.DTOs.ProjectProposalDTOs
{
    public class SendProposalDTO
    {
        public string proposal { get; set; } = string.Empty;
        //public string status { get; set; } = string.Empty;
        public Guid studentId { get; set; }
        public Guid projectId { get; set; }
    }
}
