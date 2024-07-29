namespace BridgeITAPIs.DTOs;

public class RegisterStudentDTO
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public int? RollNumber { get; set; } = null;

    public Guid UniversityId { get; set; }
}
