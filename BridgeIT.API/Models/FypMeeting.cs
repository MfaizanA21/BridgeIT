namespace BridgeITAPIs.Models;

public class FypMeeting
{
    public Guid Id { get; set; }

    public DateTime? ChosenSlot { get; set; }

    public string? Status { get; set; }

    public string? MeetLink { get; set; }

    public string? Feedback { get; set; }

    public bool IsMeetDone { get; set; }

    public Guid FypId { get; set; }

    public virtual Fyp Fyp { get; set; } = null!;

    public Guid IndExpId { get; set; }

    public virtual IndustryExpert IndustryExpert { get; set; } = null!;
}