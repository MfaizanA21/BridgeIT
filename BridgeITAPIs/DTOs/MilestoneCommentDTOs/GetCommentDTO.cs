namespace BridgeITAPIs.DTOs.MilestoneCommentDTOs;
using System.ComponentModel.DataAnnotations;

public class GetCommentDTO
{
    public Guid Id {get; set;}
    public string Comment { get; set; } = string.Empty;
    public DateTime CommentDate { get; set; }
    public Guid Commenter_id { get; set; }
    public Guid Milestone_id { get; set; }
    public string CommenterName { get; set; } = string.Empty;
}