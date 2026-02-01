using System;
using System.Collections.Generic;

namespace BridgeIT.Domain.Models;

public partial class Skill
{
    public Guid Id { get; set; }

    public string? Skill1 { get; set; }

    //public DateTime? CreatedAt { get; set; }

    //public Guid? CreatedBy { get; set; }

    //public virtual User? CreatedByNavigation { get; set; }

   // public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
