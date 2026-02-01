using System;
using System.Collections;
using System.Collections.Generic;

namespace BridgeIT.Domain.Models;

public partial class Fyp
{
    public Guid Id { get; set; }//

    public string? Title { get; set; }//

    public int? Members { get; set; }//

    public string? Batch { get; set; }//

    public string? Technology { get; set; }//

    public string? Description { get; set; }//
    
    public string fyp_id { get; set; }//
    
    public string? Status { get; set; }//

    public Guid? FacultyId { get; set; }
    
    public int? YearOfCompletion { get; set; }
    
    // public Guid? UniId { get; set; }

    public virtual ICollection<FypMeeting> FypMeetings { get; set; } = new List<FypMeeting>();
    public virtual ICollection<BoughtFyp> BoughtFyps { get; set; } = new List<BoughtFyp>();

    public virtual Faculty? Faculty { get; set; }
    // public virtual University? University { get; set; }

    public virtual ICollection<RequestForFyp> RequestForFyps { get; set; } = new List<RequestForFyp>();
    
    public virtual ICollection<SponsoredFyp> SponsoredFyps { get; set; } = new List<SponsoredFyp>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
