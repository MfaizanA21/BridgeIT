using System.ComponentModel.DataAnnotations;

namespace BridgeIT.Domain.Models;

public class ProjectModule
{
    public Guid Id { get; set; }

    public Guid ProjectId { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    public bool? Status { get; set; }

    public virtual Project? Project { get; set; }
}