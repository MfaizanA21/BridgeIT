using System;
using System.Collections.Generic;

namespace BridgeITAPIs.Models;

public partial class Project
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public int? Team { get; set; }

    public string? Stack { get; set; }

    public DateOnly? StartDate { get; set; }

    public string? CurrentStatus { get; set; }

    public DateOnly? EndDate { get; set; }

    public int? Budget { get; set; }
    
    public string? Link { get; set; }

    public Guid? StudentId { get; set; }

    public Guid? IndExpertId { get; set; }

    public Guid? FacultyId { get; set; }

    public virtual IndustryExpert? IndExpert { get; set; }

    public virtual ICollection<MileStone> MileStones { get; set; } = new List<MileStone>();

    public virtual ICollection<ProjectImage> ProjectImages { get; set; } = new List<ProjectImage>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<ProjectProposal> Proposals { get; set; } = new List<ProjectProposal>();

    public virtual Student? Student { get; set; }

    public virtual Faculty? Faculty { get; set; }
}
