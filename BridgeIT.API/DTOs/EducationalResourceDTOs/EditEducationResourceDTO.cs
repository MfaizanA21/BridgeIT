using System.ComponentModel.DataAnnotations;

namespace BridgeIT.API.DTOs.EducationalResourceDTOs;

public class EditEducationalResourceDTO
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? SourceLink { get; set; }

}