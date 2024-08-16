namespace BridgeITAPIs.DTOs;

public class GetFacultyDTO
{
    public Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public Guid? uniId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public byte[] ImageData { get; set; } = Array.Empty<byte>();
    public List<string> Interest { get; set; } = new List<string>();
    public string Post { get; set; } = string.Empty;
    public string UniversityName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

}
