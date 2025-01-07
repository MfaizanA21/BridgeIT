namespace BridgeITAPIs.DTOs.FypDTOs.DetailedFypDTOs;

public class StudentDTO
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Department { get; set; }
    public int? RollNumber { get; set; }
    public string? CvLink { get; set; }
    public string? Skills { get; set; }
}