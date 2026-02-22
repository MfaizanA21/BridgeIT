using BridgeIT.Application.Abstractions.Repository.Interface;
using BridgeIT.Application.Abstractions.Service.Interface;
using BridgeIT.Application.Common.Result;
using BridgeIT.Application.Common.Unit;
using BridgeIT.Application.DTOs;
using BridgeIT.Domain.Enums;

namespace BridgeIT.Application.Services.Implementation;

public class ProjectCompletionRequestsService : IProjectCompletionRequestsService
{
    private readonly IProjectCompletionRequestsRepository _repository;

    public ProjectCompletionRequestsService(IProjectCompletionRequestsRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> CreateRequestAsync(Guid projectId)
    {
        var existing = await _repository.GetByProjectIdAsync(projectId);
        if (existing != null && existing.RequestStatus != ProjectRequestStatus.REJECTED.ToString())
        {
            return Result<Guid>.Conflict("A completion request already exists and is not rejected.");
        }

        var request = await _repository.CreateAsync(projectId);
        return Result<Guid>.Success(request.Id);
    }

    public async Task<Result<List<ProjectCompletionRequestDto>>> GetRequestsForUserAsync(Guid userRelatedId)
    {
        var requests = await _repository.GetByUserIdAsync(userRelatedId);

        if (!requests.Any())
        {
            return Result<List<ProjectCompletionRequestDto>>.NotFound("No completion requests found for this user.");
        }

        var dtos = requests.Select(r => new ProjectCompletionRequestDto
        {
            Id = r.Id,
            ProjectId = r.ProjectId,
            ProjectName = r.Project?.Title ?? "N/A",
            ProjectDescription = r.Project?.Description ?? "N/A",
            IndExpertId = r.Project?.IndExpertId ?? Guid.Empty,
            StudentId = r.Project?.StudentId ?? Guid.Empty,
            StudentName = r.Project?.Student?.User != null
                ? $"{r.Project.Student.User.FirstName} {r.Project.Student.User.LastName}"
                : "Unknown"
        }).ToList();

        return Result<List<ProjectCompletionRequestDto>>.Success(dtos);
    }

    public async Task<Result<ProjectCompletionRequestDto>> GetRequestForProjectAsync(Guid projectId)
    {
        var request = await _repository.GetByProjectIdAsync(projectId);

        if (request == null)
        {
            return Result<ProjectCompletionRequestDto>.NotFound("No completion request found for this project.");
        }

        var dto = new ProjectCompletionRequestDto
        {
            Id = request.Id,
            ProjectId = request.ProjectId,
            ProjectName = request.Project?.Title ?? "N/A",
            ProjectDescription = request.Project?.Description ?? "N/A",
            IndExpertId = request.Project?.IndExpertId ?? Guid.Empty,
            StudentId = request.Project?.StudentId ?? Guid.Empty,
            StudentName = request.Project?.Student?.User != null
                ? $"{request.Project.Student.User.FirstName} {request.Project.Student.User.LastName}"
                : "Unknown"
        };

        return Result<ProjectCompletionRequestDto>.Success(dto);
    }

    public async Task<Result<Unit>> HandleRequestAsync(Guid requestId, string status)
    {
        if (status != ProjectRequestStatus.ACCEPTED.ToString() &&
            status != ProjectRequestStatus.REJECTED.ToString())
        {
            return Result<Unit>.BadRequest("Invalid status. Must be ACCEPTED or REJECTED.");
        }

        var request = await _repository.GetByIdAsync(requestId);
        if (request == null)
        {
            return Result<Unit>.NotFound("Request not found.");
        }

        if (request.RequestStatus != ProjectRequestStatus.PENDING.ToString())
        {
            return Result<Unit>.Conflict("Request is no longer pending.");
        }

        request.RequestStatus = status;
        await _repository.UpdateStatusAsync(request);

        return Result<Unit>.Success(Unit.Value);
    }
}