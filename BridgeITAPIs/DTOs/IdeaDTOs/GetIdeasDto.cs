namespace BridgeITAPIs.DTOs.IdeaDTOs;

public class GetIdeasDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Technology { get; set; }
    public string? Description { get; set; }
    public Guid? FacultyId { get; set; }
    public string? FacultyName { get; set; }
    public string? Email { get; set; }
    public Guid UniId { get; set; }
    public Guid? UserId { get; set; }
    public string? UniName { get; set; }
}