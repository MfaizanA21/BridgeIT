namespace BridgeIT.API.DTOs.UniAdminDTOs;

public class RegisterUniAdminDTO
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Contact { get; set; } = string.Empty;

    public string OfficeAddress { get; set; } = string.Empty;

    public Guid UniversityId { get; set; }
}
