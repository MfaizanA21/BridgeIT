using BridgeITAPIs.services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BridgeITAPIs.Controllers;

[ApiController]
[Route("api/request-for-project-completion")]
public class RequestForProjectCompletionController : ControllerBase
{
    private readonly BridgeItContext _dbContext;
    private readonly IProjectCompletionRequestsService _projectCompletionRequestsService;
    
    public RequestForProjectCompletionController(BridgeItContext dbContext, IProjectCompletionRequestsService projectCompletionRequestsService)
    {
        _dbContext = dbContext;
        _projectCompletionRequestsService = projectCompletionRequestsService;
    }

    [HttpPost("put-completion-request")]
    public async Task<IActionResult> PutCompletionRequest([FromBody] Guid projectId)
    {
        return await _projectCompletionRequestsService.PutCompletionRequestsAsync(projectId);
        
    }
    
    /// <summary>
    /// Retrieves the project completion request associated with the given industry-expert or student.
    /// </summary>
    /// <param name="Id">The unique identifier of the student or industry expert related to the project request.</param>
    /// <returns>An <see cref="IActionResult"/> containing the list of project completion requests or an appropriate error message.</returns>
    [HttpGet("get-completion-request/{Id}")]
    public async Task<IActionResult> GetCompletionRequest(Guid Id)
    {
        return await _projectCompletionRequestsService.GetCompletionRequestsAsync(Id);
    }
}