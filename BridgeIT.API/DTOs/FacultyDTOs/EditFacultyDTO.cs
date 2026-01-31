namespace BridgeITAPIs.DTOs.FacultyDTOs;

public class EditFacultyDTO
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public byte[] ImageData { get; set; } = Array.Empty<byte>();
    public Guid? UniversityId { get; set; }
    public List<string> Interest { get; set; } = new List<string>();
    public string Post { get; set; } = string.Empty;

}
