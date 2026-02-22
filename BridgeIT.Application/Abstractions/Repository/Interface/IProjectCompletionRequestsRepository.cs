using BridgeIT.Domain.Models;

namespace BridgeIT.Application.Abstractions.Repository.Interface;

public interface IProjectCompletionRequestsRepository
{
    Task<RequestForProjectCompletion> CreateAsync(Guid projectId);

    Task<List<RequestForProjectCompletion>> GetByUserIdAsync(Guid userRelatedId);

    Task<RequestForProjectCompletion?> GetByProjectIdAsync(Guid projectId);

    Task<RequestForProjectCompletion?> GetByIdAsync(Guid requestId);

    Task UpdateStatusAsync(RequestForProjectCompletion request);
}