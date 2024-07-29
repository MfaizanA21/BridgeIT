using System;
using System.Collections.Generic;

namespace BridgeITAPIs.Models;

public partial class ResearchWork
{
    public Guid Id { get; set; }

    public string? PaperName { get; set; }

    public string? Category { get; set; }

    public string? PublishChannel { get; set; }

    public string? OtherResearchers { get; set; }

    public DateOnly? YearOfPublish { get; set; }

    public Guid? FacultyId { get; set; }

    public virtual Faculty? Faculty { get; set; }
}
