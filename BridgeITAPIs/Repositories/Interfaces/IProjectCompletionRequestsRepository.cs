using BridgeITAPIs.Enums;
using Microsoft.AspNetCore.Mvc;

namespace BridgeITAPIs.Repositories.interfaces;

public interface IProjectCompletionRequestsRepository
{
    public Task<IActionResult> PutCompletionRequestsAsync(Guid projectId);
    
    public Task<IActionResult> GetCompletionRequestsAsync(Guid Id);
    
    public Task<IActionResult> HandleRequestAsync(Guid RequestId, string status);
}