using System;
using System.Collections.Generic;

namespace BridgeITAPIs.Models;

public partial class Faculty
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public string? Interest { get; set; }

    public string? Post { get; set; }
    
    public string? Department { get; set; }

    public Guid? UniId { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<FacultyExperience> FacultyExperiences { get; set; } = new List<FacultyExperience>();

    public virtual ICollection<Fyp> Fyps { get; set; } = new List<Fyp>();

    public virtual ICollection<Idea> Ideas { get; set; } = new List<Idea>();

    public virtual ICollection<ResearchWork> ResearchWorks { get; set; } = new List<ResearchWork>();

    public virtual University? Uni { get; set; }

    public virtual User? User { get; set; }

    //public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    //public virtual ICollection<FieldOfInterest> FieldOfInterests { get; set; } = new List<FieldOfInterest>();
}
