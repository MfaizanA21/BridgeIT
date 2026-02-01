using System;
using System.Collections.Generic;

namespace BridgeIT.Domain.Models;

public partial class University
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public int? EstYear { get; set; }
    
    public byte[]? uniImage { get; set; }

    // public ICollection<Fyp> Fyps { get; set; } = new List<Fyp>();
    
    public virtual ICollection<Faculty> Faculties { get; set; } = new List<Faculty>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<UniversityAdmin> UniversityAdmins { get; set; } = new List<UniversityAdmin>();
}
