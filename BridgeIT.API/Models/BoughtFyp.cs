using System;
using System.Collections.Generic;

namespace BridgeITAPIs.Models;

public partial class BoughtFyp
{
    public Guid Id { get; set; }

    public Guid? FypId { get; set; }

    public Guid? IndExpertId { get; set; }

    public Guid? UniversityAdminId { get; set; }

    public byte[]? Agreement { get; set; }

    public long? Price { get; set; }

    public DateOnly? PurchaseDate { get; set; }

    public virtual Fyp? Fyp { get; set; }

    public virtual IndustryExpert? IndExpert { get; set; }

    public virtual UniversityAdmin? UniversityAdmin { get; set; }
}
