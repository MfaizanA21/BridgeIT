using BridgeIT.Domain.Enums;

namespace BridgeIT.Application.DTOs;

public class CreateProjectCompletionRequestDto
{
    public Guid ProjectId { get; set; }
}

public class ProjectCompletionRequestDto
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public Guid? StudentId { get; set; }
    public Guid? IndExpertId { get; set; }
    public string ProjectName { get; set; } = String.Empty;
    public string ProjectDescription { get; set; } = String.Empty;
    public string StudentName { get; set; } = String.Empty;
}

public class UpdateRequestStatusDto
{
    public ProjectRequestStatus Status { get; set; }
}