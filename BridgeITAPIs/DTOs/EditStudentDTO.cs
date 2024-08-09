namespace BridgeITAPIs.DTOs;

public class EditStudentDTO
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string ImageData { get; set; } = string.Empty;
    public Guid? universityId { get; set; }
    public string RollNumber { get; set; } = string.Empty;
}
