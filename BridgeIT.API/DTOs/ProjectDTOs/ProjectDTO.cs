namespace BridgeITAPIs.DTOs.ProjectDTOs
{
    public class ProjectDTO
    {
        public Guid Id { get; set; }
        public Guid? StudentId { get; set; }
        public Guid? IndExpertId { get; set; }
        public Guid? StdUserId { get; set; }
        public Guid? IExptUserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Stack { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string StartDate { get; set; } = string.Empty;
        public string EndDate { get; set; } = string.Empty;
        public string studentName { get; set; } = string.Empty;
        public string expertName { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
    }
}
