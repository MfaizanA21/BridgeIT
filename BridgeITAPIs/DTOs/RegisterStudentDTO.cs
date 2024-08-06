namespace BridgeITAPIs.DTOs;

public class RegisterStudentDTO
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;
    public List<string> Skills { get; set; } = new List<string>();

    public string Password { get; set; } = string.Empty;
    public string ImageData { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;

    public int? RollNumber { get; set; } = null;

    public Guid UniversityId { get; set; }
}
