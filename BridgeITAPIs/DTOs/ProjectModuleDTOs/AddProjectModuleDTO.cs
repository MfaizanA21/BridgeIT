using System.ComponentModel.DataAnnotations;

namespace BridgeITAPIs.DTOs.ProjectModuleDTOs;

public class AddProjectModuleDTO
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; } = string.Empty;
}