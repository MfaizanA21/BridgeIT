namespace BridgeIT.API.DTOs.FypDTOs;

public class GetFypForFacultyDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string FypId { get; set; } = string.Empty;
    public int? Members { get; set; }
    public string Description { get; set; } = String.Empty;
}