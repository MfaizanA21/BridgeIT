namespace BridgeIT.API.DTOs.ProjectCompletionRequestDTOs;

public class GetProjectCompletionRequestDto
{
     public Guid Id { get; set; }
     public Guid ProjectId { get; set; }
     public Guid? StudentId { get; set; }
     public Guid? IndExpertId { get; set; }
     public string ProjectName { get; set; } = String.Empty;
     public string ProjectDescription { get; set; } = String.Empty;
     public string StudentName { get; set; } = String.Empty;
}