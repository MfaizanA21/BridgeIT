namespace BridgeIT.API.DTOs.FypMeetingDtos;

public class GetSponsoredFypDto
{
    public Guid Id { get; set; }
    public Guid? FypId { get; set; }
    public string AgreementBase64 { get; set; } = String.Empty;
    public Guid? SponsoredById { get; set; }
    public string SponsoredByName { get; set; } = String.Empty;
    
}