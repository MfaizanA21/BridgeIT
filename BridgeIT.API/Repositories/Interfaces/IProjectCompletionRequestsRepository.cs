using BridgeIT.API.Enums;
using Microsoft.AspNetCore.Mvc;

namespace BridgeIT.API.Repositories.interfaces;

public interface IProjectCompletionRequestsRepository
{
    public Task<IActionResult> PutCompletionRequestsAsync(Guid projectId);
    
    public Task<IActionResult> GetCompletionRequestsAsync(Guid Id);
    
    public Task<IActionResult> GetCompletionRequestForProjectAsync(Guid Id);
    
    public Task<IActionResult> HandleRequestAsync(Guid RequestId, string status);
}