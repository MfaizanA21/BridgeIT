using System;
using System.Collections.Generic;

namespace BridgeITAPIs.Models;

public partial class MileStone
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateOnly? AchievementDate { get; set; }

    public Guid? ProjectId { get; set; }

    public virtual Project? Project { get; set; }
    
    public ICollection<MilestoneComment> MilestoneComments { get; set; } = new List<MilestoneComment>();
}
