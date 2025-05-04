namespace BridgeITAPIs.DTOs.FypDTOs.DetailedFypDTOs;

public class DetailedFypDTO
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public int? Members { get; set; }
    public string? Batch { get; set; }
    public string? Technology { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }
    public int ? YearOfCompletion { get; set; }

    public FacultyDTO? Faculty { get; set; }
    public List<StudentDTO> Students { get; set; } = new List<StudentDTO>();
}