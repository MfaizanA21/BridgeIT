using System;
using System.Collections.Generic;

namespace BridgeIT.Domain.Models;

public partial class Review
{
    public Guid Id { get; set; }

    public Guid? ProjectId { get; set; }

    public Guid? ReviewerId { get; set; }

    public string? Review1 { get; set; }

    public DateTime? DatePosted { get; set; }

    public int? Rating { get; set; }

    public virtual Project? Project { get; set; }

    public virtual User? Reviewer { get; set; }
}
