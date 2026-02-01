namespace BridgeIT.API.DTOs.StudentDTOs;

public class EditStudentDTO
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public byte[] ImageData { get; set; } = Array.Empty<byte>();
    public Guid? universityId { get; set; }
    public string RollNumber { get; set; } = string.Empty;
    public string CvLink { get; set; } = String.Empty; 
}
