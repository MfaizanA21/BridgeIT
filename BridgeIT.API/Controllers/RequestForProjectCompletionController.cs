using BridgeIT.API.Extension;
using BridgeIT.Application.Abstractions.Service.Interface;
using BridgeIT.Application.Common.Result;
using BridgeIT.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BridgeIT.API.Controllers;

[ApiController]
[Route("api/project-completion-requests")]  // ← more RESTful plural resource name
public class ProjectCompletionRequestsController : ControllerBase
{
    private readonly IProjectCompletionRequestsService _service;

    public ProjectCompletionRequestsController(IProjectCompletionRequestsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Creates a new project completion request for a given project.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] Guid projectId)
    {
        var result = await _service.CreateRequestAsync(projectId);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Retrieves all project completion requests related to a student or industry expert.
    /// </summary>
    /// <param name="userId">The GUID of the student or industry expert</param>
    [HttpGet("user/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProjectCompletionRequestDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetForUser(Guid userId)
    {
        var result = await _service.GetRequestsForUserAsync(userId);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Retrieves the project completion request for a specific project.
    /// </summary>
    /// <param name="projectId">The GUID of the project</param>
    [HttpGet("project/{projectId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectCompletionRequestDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetForProject(Guid projectId)
    {
        var result = await _service.GetRequestForProjectAsync(projectId);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Updates the status of a project completion request (ACCEPTED or REJECTED only).
    /// </summary>
    /// <param name="requestId">The GUID of the completion request</param>
    /// <param name="status">Must be exactly "ACCEPTED" or "REJECTED"</param>
    [HttpPatch("{requestId}/status")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateStatus(Guid requestId, [FromBody] string status)
    {
        var result = await _service.HandleRequestAsync(requestId, status);
        return result.ToActionResult(this);
    }
    
}