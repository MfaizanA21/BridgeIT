namespace BridgeITAPIs.DTOs
{
    public class GetResearchPaperDTO
    {
        public Guid Id { get; set; }
        public string PaperName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string PublishChannel { get; set; } = string.Empty;
        public string OtherResearchers { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public DateOnly? YearOfPublish { get; set; }
        public Guid? FacultyId { get; set; }
        public string FacultyName { get; set; } = string.Empty;

    }
}
