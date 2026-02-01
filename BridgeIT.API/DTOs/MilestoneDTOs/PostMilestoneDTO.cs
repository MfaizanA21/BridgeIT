using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;

namespace BridgeIT.API.DTOs.MilestoneDTOs;


public class PostMilestoneDTO
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateOnly AchievementDate { get; set; }
}