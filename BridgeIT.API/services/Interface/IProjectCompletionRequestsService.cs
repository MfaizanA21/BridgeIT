using BridgeIT.API.Enums;
using Microsoft.AspNetCore.Mvc;

namespace BridgeIT.API.services.Interface;

public interface IProjectCompletionRequestsService
{
    Task<IActionResult> PutCompletionRequestsAsync(Guid projectId);

    Task<IActionResult> GetCompletionRequestsAsync(Guid Id);

    Task<IActionResult> GetCompletionRequestForProjectAsync(Guid Id);
    
    Task<IActionResult> HandleRequestAsync(Guid RequestId, string status);
}