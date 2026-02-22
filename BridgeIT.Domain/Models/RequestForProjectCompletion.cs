using BridgeIT.Domain.Enums;

namespace BridgeIT.Domain.Models;

public class RequestForProjectCompletion
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string RequestStatus { get; set; } = ProjectRequestStatus.PENDING.ToString();
    public virtual Project? Project { get; set; }

}
