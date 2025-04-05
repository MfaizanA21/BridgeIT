namespace BridgeITAPIs.DTOs.ReviewDTOs;

public class AddReviewDto
{
    public Guid ReviewerId { get; set; }
    public string Review { get; set; } = string.Empty;
    public int Rating { get; set; }
}