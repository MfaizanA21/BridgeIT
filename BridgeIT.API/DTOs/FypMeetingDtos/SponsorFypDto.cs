namespace BridgeIT.API.DTOs.FypMeetingDtos;

public class SponsorFypDto
{
    public Guid ExpertId { get; set; }
    public string AgreementBase64 { get; set; } = null!;
}