using System.ComponentModel.DataAnnotations;

namespace BridgeIT.API.DTOs.IdeaDTOs;


public class AddIdeaDto
{
    [Required]
    public string? Title { get; set; } 
    [Required]
    public string? Technology { get; set; } 
    [Required]
    public string? Description { get; set; }
    // [Required]
    // public Guid? FacultyIdea { get; set; }
}