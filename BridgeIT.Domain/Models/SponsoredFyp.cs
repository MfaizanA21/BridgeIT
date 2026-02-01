using System;
using System.Collections.Generic;

namespace BridgeIT.Domain.Models;

public partial class SponsoredFyp
{
    public Guid Id { get; set; }

    public Guid? FypId { get; set; }

    public Guid? SponsoredById { get; set; }

    public byte[]? Agreement { get; set; }

    public virtual Fyp? Fyp { get; set; }

    public virtual IndustryExpert? SponsoredBy { get; set; }
}
