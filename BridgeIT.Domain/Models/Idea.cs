using System;
using System.Collections.Generic;

namespace BridgeIT.Domain.Models;

public partial class Idea
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Technology { get; set; }

    public string? Description { get; set; }

    public Guid? FacultyId { get; set; }

    public virtual Faculty? Faculty { get; set; }

    public virtual ICollection<InterestedForIdea> InterestedForIdeas { get; set; } = new List<InterestedForIdea>();
}
