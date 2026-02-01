using System.ComponentModel.DataAnnotations;

namespace BridgeIT.API.DTOs.MilestoneDTOs;

public class UpdateMilestoneDTO
{
    
    public string? Title { get; set; }
    
    public string? Description {get; set; }

    public DateOnly? AchievementDate { get; set; }
}