using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BridgeIT.API.DTOs.StudentDTOs;

public class GetStudentDTO
{
    public Guid? userId { get; set; }
    public Guid Id { get; set; }
    public Guid? universityId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<string> Skills { get; set; } = new List<string>();
    public string Department { get; set; } = string.Empty;
    public byte[] ImageData { get; set; } = Array.Empty<byte>();
    public string Description { get; set; } = string.Empty;
    public string UniversityName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public byte[] UniImage { get; set; } = Array.Empty<byte>();
    public string RollNumber { get; set; } = string.Empty;
    
    public Guid? FypId { get; set; }
}
