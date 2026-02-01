namespace BridgeIT.API.DTOs.FypDTOs;

public class GetFypByIdDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string FypId { get; set; } = string.Empty;
    public int? Members { get; set; }
    public string Batch { get; set; } = string.Empty;
    public string Technology { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public int ? YearOfCompletion { get; set; }
    
}