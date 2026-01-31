namespace BridgeITAPIs.DTOs.EducationalResourceDTOs;

public class GetEducationalRedourcesDTO
{
    public Guid id { get; set; }
    public string title { get; set; } = string.Empty;
    public string content { get; set; } = string.Empty;
    public string? sourceLink { get; set; }
    public Guid facultyId { get; set; }
    public string facultyName { get; set; } = string.Empty;
    public string facultyPost { get; set; } = string.Empty;
    public string facultyDepartment { get; set; } = string.Empty;
    public Guid universityId { get; set; }
    public string universityName { get; set; } = string.Empty;
    public string universityLocation { get; set; } = string.Empty;
}