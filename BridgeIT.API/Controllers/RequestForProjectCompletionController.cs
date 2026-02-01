using BridgeIT.API.services.Interface;
using Microsoft.AspNetCore.Mvc;


namespace BridgeIT.API.Controllers;

[ApiController]
[Route("api/request-for-project-completion")]
public class RequestForProjectCompletionController : ControllerBase
{
    private readonly IProjectCompletionRequestsService _projectCompletionRequestsService;
    
    public RequestForProjectCompletionController(IProjectCompletionRequestsService projectCompletionRequestsService)
    {
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
    /// <param name="Id">The GUID of the student or industry-expert related to the project request.</param>
    /// <returns>An <see cref="IActionResult"/> containing the list of project completion requests or an appropriate error message.</returns>
    [HttpGet("get-completion-request/{Id}")]
    public async Task<IActionResult> GetCompletionRequest(Guid Id)
    {
        return await _projectCompletionRequestsService.GetCompletionRequestsAsync(Id);
    }
    
    
    /// <summary>
    /// Retrieves the project completion request associated with the given project. 
    /// </summary>
    /// <param name="Id">The GUID of the project related to the project request</param>
    /// <returns>An <see cref="IActionResult"/> containing the project completion request or an appropriate error message.</returns>
    [HttpGet("completion-request-for-project/{Id}")]
    public async Task<IActionResult> GetCompletionRequestForProject(Guid Id)
    {
        return await _projectCompletionRequestsService.GetCompletionRequestForProjectAsync(Id);
    }
    
    /// <summary>
    /// Changes the Project status to either "Accepted" or "Rejected" based on the body.
    /// </summary>
    /// <param name="RequestId">The GUID of the request</param>
    /// <param name="status"> status it should strictly be either ACCEPTED or REJECTED no other will be accepted. </param>
    /// <returns>An <see cref="IActionResult"/> containing the list of project completion requests or an appropriate error message.</returns>
    [HttpPatch("handle-request/{RequestId}")]
    public async Task<IActionResult> HandleRequest([FromRoute] Guid RequestId, [FromBody] string status)
    {
        return await _projectCompletionRequestsService.HandleRequestAsync(RequestId, status);
    }
}