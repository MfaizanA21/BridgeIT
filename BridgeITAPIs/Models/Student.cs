using System;
using System.Collections.Generic;

namespace BridgeITAPIs.Models;

public partial class Student
{
    public Guid Id { get; set; }

    public string? skills { get; set; }

    public int? RollNumber { get; set; }

    public string? department { get; set; }

    public string? cvLink { get; set; }

    public Guid? UserId { get; set; }

    public Guid? UniversityId { get; set; }
    
    public Guid? FypId { get; set; }

    public virtual ICollection<DegreeReport> DegreeReports { get; set; } = new List<DegreeReport>();

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual Fyp? Fyp { get; set; }
    
    public virtual University? University { get; set; }

    public virtual User? User { get; set; }

    public virtual ICollection<RequestForFyp> RequestForFyps { get; set; } = new List<RequestForFyp>();
    // public virtual ICollection<Fyp> Fyps { get; set; } = new List<Fyp>();

    public virtual ICollection<ProjectProposal> Proposals { get; set; } = new List<ProjectProposal>();
    
    public virtual ICollection<InterestedForIdea> InterestedForIdeas { get; set; } = new List<InterestedForIdea>();
    
}
