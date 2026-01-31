namespace BridgeITAPIs.DTOs.FypDTOs;

public class GetFypsRequestsForUniAdminDTO
{
    public Guid FId { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public string FypId { get; set; } = string.Empty;
    
    public int? Members { get; set; }
    
    public string Batch { get; set; } = string.Empty;
    
    public string Technology { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public string Status { get; set; } = string.Empty;
    
    public Guid StudentId { get; set; }
    
    public string StudentName { get; set; } = string.Empty;
    
    public string StudentEmail { get; set; } = string.Empty;
    
    public int? StudentRollNo { get; set; }
    
    // public Byte[]? StudentImage { get; set; }
    
    public Guid UniId { get; set; }
    
    public string UniName { get; set; } = string.Empty;
    
}