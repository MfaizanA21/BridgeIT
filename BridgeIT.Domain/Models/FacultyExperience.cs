using System;
using System.Collections.Generic;

namespace BridgeIT.Domain.Models;

public partial class FacultyExperience
{
    public Guid Id { get; set; }

    public Guid? CompanyId { get; set; }

    public string? JobTitle { get; set; }

    public string? Duration { get; set; }

    public string? Responsibilities { get; set; }

    public Guid? FacultyId { get; set; }

    public virtual Company? Company { get; set; }

    public virtual Faculty? Faculty { get; set; }
}
