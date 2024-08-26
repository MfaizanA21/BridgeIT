namespace BridgeITAPIs.DTOs
{
    public class GetEventDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string SpeakerName { get; set; } = string.Empty;
        public DateTime? EventDate { get; set; }
        public string Venue { get; set; } = string.Empty;
        public Guid? FacultyId { get; set; }
    }
}
