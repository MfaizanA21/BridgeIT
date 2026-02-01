using System;
using System.Collections.Generic;

namespace BridgeIT.Domain.Models;

public partial class Event
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public string? SpeakerName { get; set; }

    public DateTime? EventDate { get; set; }

    public string? Venue { get; set; }

    public string? Description { get; set; }

    public Guid? FacultyId { get; set; }

    public virtual Faculty? Faculty { get; set; }
}
