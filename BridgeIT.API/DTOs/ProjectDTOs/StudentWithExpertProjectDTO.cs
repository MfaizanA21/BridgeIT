namespace BridgeIT.API.DTOs.ProjectDTOs;

public class StudentWithExpertProjectDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string? EndDate { get; set; } = string.Empty;
    public int Budget { get; set; }
    public Guid? StudentId { get; set; }
    public string studentName { get; set; } = string.Empty;
    public Guid? IndExpertId { get; set; }
    public string expertName { get; set; } = string.Empty;
}
