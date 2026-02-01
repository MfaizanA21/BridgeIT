using System;
using System.Collections;
using System.Collections.Generic;

namespace BridgeIT.Domain.Models;

public partial class IndustryExpert
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public string? Contact { get; set; }

    public string? Post { get; set; }

    public Guid? CompanyId { get; set; }
    
    public virtual ICollection<FypMeeting> FypMeetings { get; set; } = new List<FypMeeting>();

    public virtual ICollection<RequestForFyp> RequestForFyps { get; set; } = new List<RequestForFyp>();

    public virtual ICollection<BoughtFyp> BoughtFyps { get; set; } = new List<BoughtFyp>();

    public virtual Company? Company { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual ICollection<SponsoredFyp> SponsoredFyps { get; set; } = new List<SponsoredFyp>();

    //public virtual ICollection<ProjectProposal> Proposals { get; set; } = new List<ProjectProposal>();
    
    public ICollection<MilestoneComment> MilestoneComments { get; set; } = new List<MilestoneComment>();

    public virtual User? User { get; set; }
}
