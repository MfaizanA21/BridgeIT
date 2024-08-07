namespace BridgeITAPIs.DTOs;

public class GetStudentDTO
{
    public Guid userId { get; set; }
    public Guid Id { get; set; }
    public Guid universityId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<string> Skills { get; set; } = new List<string>();
    public string ImageData { get; set; } = string.Empty;
    public string UniversityName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string RollNumber { get; set; } = string.Empty;
}
