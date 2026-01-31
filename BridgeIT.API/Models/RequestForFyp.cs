namespace BridgeITAPIs.Models;

public class RequestForFyp
{
    public Guid Id { get; set; }
    
    public int? Status { get; set; }
    
    public Guid? StudentId { get; set; }
    
    public Guid FypId { get; set; }
    
    public Guid IndustryExpertId { get; set; }
    
    public virtual IndustryExpert? IndustryExpert { get; set; }
    
    public virtual Student? Student { get; set; }
    
    public virtual Fyp? Fyp { get; set; }
}