namespace BridgeITAPIs.DTOs.ProjectProposalDTOs
{
    public class SendProposalDTO
    {
        public IFormFile proposal { get; set; } = null!;
        //public string status { get; set; } = string.Empty;
        public Guid studentId { get; set; }
        public Guid projectId { get; set; }
    }
}
