using System;
using System.Collections.Generic;

namespace BridgeIT.Domain.Models;

public partial class Company
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Business { get; set; }

    public string? Address { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<FacultyExperience> FacultyExperiences { get; set; } = new List<FacultyExperience>();

    public virtual ICollection<IndustryExpert> IndustryExperts { get; set; } = new List<IndustryExpert>();
}
