namespace BridgeIT.API.DTOs.UniversityDTOs;

public class GetUniversityDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int? EstYear { get; set; }
    public byte[]? uniImage { get; set; } = Array.Empty<byte>();
}
