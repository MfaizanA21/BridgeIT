namespace BridgeIT.API.DTOs.ChatDTOs;

public class SendMessageDTO
{
    public Guid StudentId { get; set; }
    public Guid ExpertId { get; set; }
    public string Message { get; set; } = String.Empty; 
}