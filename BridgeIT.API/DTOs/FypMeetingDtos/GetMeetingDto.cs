namespace BridgeITAPIs.DTOs.FypMeetingDtos;

public class GetMeetingDto
{
    public Guid id { get; set; }
    public string? status { get; set; }
    public string? meetLink { get; set; }
    public string? feedback { get; set; }
    public bool isMeetDone { get; set; }
    public DateTime? chosenSlot { get; set; }
    public Guid fypId { get; set; }
    public string? fypTitle { get; set; }
    public Guid indExpId { get; set; }
    
}