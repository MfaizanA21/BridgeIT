namespace BridgeITAPIs.DTOs
{
    public class RegisterResearchDTO
    {
        public string paperName { get; set; } = string.Empty;
        public string category { get; set; } = string.Empty;
        public string publishChannel { get; set; } = string.Empty;
        public string otherResearchers { get; set; } = string.Empty;
        public string link { get; set; } = string.Empty;
        public DateOnly yearOfPublish { get; set; }
        public Guid facultyId { get; set; }

    }
}
