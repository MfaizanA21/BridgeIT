using System;
using System.Collections.Generic;

namespace BridgeITAPIs.Models;

public partial class Fyp
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public int? Members { get; set; }

    public string? Batch { get; set; }

    public string? Technology { get; set; }

    public string? Description { get; set; }

    public Guid? FacultyId { get; set; }

    public virtual ICollection<BoughtFyp> BoughtFyps { get; set; } = new List<BoughtFyp>();

    public virtual Faculty? Faculty { get; set; }

    public virtual ICollection<SponsoredFyp> SponsoredFyps { get; set; } = new List<SponsoredFyp>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
