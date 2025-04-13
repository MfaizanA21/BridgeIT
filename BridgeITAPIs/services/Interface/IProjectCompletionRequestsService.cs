using Microsoft.AspNetCore.Mvc;

namespace BridgeITAPIs.services.Interface;

public interface IProjectCompletionRequestsService
{
    Task<IActionResult> PutCompletionRequestsAsync(Guid projectId);

    Task<IActionResult> GetCompletionRequestsAsync(Guid Id);
}