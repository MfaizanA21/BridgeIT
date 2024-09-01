namespace BridgeITAPIs.DTOs.ProjectDTOs;

public class StudentAddProjectDTO
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Team { get; set; } = 0;
    public string Stack { get; set; } = string.Empty;
    public string CurrentStatus { get; set; } = string.Empty;
    public string StartDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
    public Guid? StudentId { get; set; }
    //public Guid? IndExpertId { get; set; }
}
