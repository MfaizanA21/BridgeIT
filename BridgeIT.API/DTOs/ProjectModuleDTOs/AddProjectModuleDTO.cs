using System.ComponentModel.DataAnnotations;

namespace BridgeIT.API.DTOs.ProjectModuleDTOs;

public class AddProjectModuleDTO
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; } = string.Empty;
}