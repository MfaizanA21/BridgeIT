using System.ComponentModel.DataAnnotations;

namespace BridgeITAPIs.DTOs.EducationalResourceDTOs;

public class AddEducationalResourceDTO
{
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required]
    public string Content { get; set; } = string.Empty;
    public string? SourceLink { get; set; }
    public Guid FacultyId { get; set; }
}