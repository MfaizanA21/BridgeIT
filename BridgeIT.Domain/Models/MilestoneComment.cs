namespace BridgeIT.Domain.Models;

public class MilestoneComment
{
    public Guid Id { get; set; }
    public string Comment { get; set; } = String.Empty;
    public DateTime CommentDate { get; set; }
    public Guid Commenter_id { get; set; }
    public Guid Milestone_id { get; set; }
    public virtual MileStone MileStone { get; set; }
    public virtual IndustryExpert Commenter { get; set; }
}