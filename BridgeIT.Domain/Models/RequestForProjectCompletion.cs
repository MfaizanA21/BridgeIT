using BridgeIT.Domain.Enums;

namespace BridgeIT.Domain.Models;

public class RequestForProjectCompletion
{
    public Guid id { get; set; }
    public Guid ProjectId { get; set; }
    public string RequestStatus { get; set; } = ProjectRequestStatus.PENDING.ToString();
    public virtual Project? project { get; set; }

}
