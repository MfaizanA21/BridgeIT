namespace BridgeITAPIs.DTOs.InterestedForIdeaDTOs;

public class GetRequestDetailDTO
{
    public Guid Id { get; set; }
    public Guid IdeaId { get; set; }
    public Guid? StudentId { get; set; }
    public Guid? FacultyId { get; set; }
    public Guid? StdUserId { get; set; }
    public Guid? FacUserId { get; set; }
    public string IdeaName { get; set; } = string.Empty;
    public string StudentName { get; set; } = string.Empty;
    public string StudentDept { get; set; } = string.Empty;
    public string StudentEmail { get; set; } = string.Empty;
    public int? StudentRollNumber { get; set; }
}