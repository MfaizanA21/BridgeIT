using System;
using System.Collections.Generic;

namespace BridgeITAPIs.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Role { get; set; }

    public string? Hash { get; set; }

    public string? Salt { get; set; }

    public string? ImageData { get; set; }

    public virtual ICollection<Faculty> Faculties { get; set; } = new List<Faculty>();

    public virtual ICollection<IndustryExpert> IndustryExperts { get; set; } = new List<IndustryExpert>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<UniversityAdmin> UniversityAdmins { get; set; } = new List<UniversityAdmin>();
}
