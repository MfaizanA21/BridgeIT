using System;
using System.Collections.Generic;

namespace BridgeIT.Domain.Models;

public partial class UniversityAdmin
{
    public Guid Id { get; set; }

    public string? Contact { get; set; }

    public string? OfficeAddress { get; set; }

    public Guid? UserId { get; set; }

    public Guid? UniId { get; set; }

    public virtual ICollection<BoughtFyp> BoughtFyps { get; set; } = new List<BoughtFyp>();

    public virtual University? Uni { get; set; }

    public virtual User? User { get; set; }
}
