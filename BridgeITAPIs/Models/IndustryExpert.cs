using System;
using System.Collections.Generic;

namespace BridgeITAPIs.Models;

public partial class IndustryExpert
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public string? Contact { get; set; }

    public string? Post { get; set; }

    public Guid? CompanyId { get; set; }

    public virtual ICollection<BoughtFyp> BoughtFyps { get; set; } = new List<BoughtFyp>();

    public virtual Company? Company { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual ICollection<SponsoredFyp> SponsoredFyps { get; set; } = new List<SponsoredFyp>();

    public virtual User? User { get; set; }
}
