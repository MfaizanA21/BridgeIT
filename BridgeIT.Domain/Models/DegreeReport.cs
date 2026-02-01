using System;
using System.Collections.Generic;

namespace BridgeIT.Domain.Models;

public partial class DegreeReport
{
    public Guid Id { get; set; }

    public double? Gpa { get; set; }

    public string? Program { get; set; }

    public string? Achievements { get; set; }

    public string? Activities { get; set; }

    public Guid? StudentId { get; set; }

    public virtual Student? Student { get; set; }
}
