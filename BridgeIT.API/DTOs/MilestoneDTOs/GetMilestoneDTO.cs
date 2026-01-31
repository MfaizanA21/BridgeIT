namespace BridgeITAPIs.DTOs.MilestoneDTOs;

public class GetMilestoneDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateOnly? AchievementDate { get; set; }
    public Guid? ProjectId { get; set; }
}