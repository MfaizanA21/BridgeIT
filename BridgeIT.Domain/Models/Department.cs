using System;
using System.Collections.Generic;

namespace BridgeIT.Domain.Models;

public partial class Department
{
    public Guid Id { get; set; }

    public string? Department1 { get; set; }

    //public virtual ICollection<Faculty> Faculties { get; set; } = new List<Faculty>();
}
