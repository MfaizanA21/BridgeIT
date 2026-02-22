namespace BridgeIT.Application.DTOs;

public class BoughtFypDetailDto
{
    public Guid Id { get; set; }
    public Guid FypId { get; set; }
    public Guid IndExpertId { get; set; }
    public long Price { get; set; }
    public string FypTitle { get; set; } = string.Empty;
    public string IndExpertName { get; set; } = string.Empty;
}