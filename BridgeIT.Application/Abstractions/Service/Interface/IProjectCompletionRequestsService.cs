using BridgeIT.Application.Common.Result;
using BridgeIT.Application.Common.Unit;
using BridgeIT.Application.DTOs;

namespace BridgeIT.Application.Abstractions.Service.Interface;

public interface IProjectCompletionRequestsService
{
    Task<Result<Guid>> CreateRequestAsync(Guid projectId);

    Task<Result<List<ProjectCompletionRequestDto>>> GetRequestsForUserAsync(Guid userRelatedId);

    Task<Result<ProjectCompletionRequestDto>> GetRequestForProjectAsync(Guid projectId);

    Task<Result<Unit>> HandleRequestAsync(Guid requestId, string status);
}