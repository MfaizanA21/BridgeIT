namespace BridgeITAPIs.DTOs;

public class RegisterFacultyDTO
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    //public string Description { get; set; } = string.Empty;
    public List<string> Interest { get; set; } = new List<string>();
    public string Post { get; set; } = string.Empty;
    public Guid UniversityId { get; set; }
}
