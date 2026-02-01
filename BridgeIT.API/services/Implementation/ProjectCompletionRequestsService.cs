using BridgeIT.API.Enums;
using BridgeIT.API.Repositories.interfaces;
using BridgeIT.API.services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BridgeIT.Infrastructure;

namespace BridgeIT.API.services.Implementation;

public class ProjectCompletionRequestsService: IProjectCompletionRequestsService
{
    private readonly BridgeItContext _dbContext;
    private readonly IProjectCompletionRequestsRepository _projectCompletionRequestRepository;
    
    public ProjectCompletionRequestsService(BridgeItContext dbContext, IProjectCompletionRequestsRepository projectCompletionRequestRepository)
    {
        _dbContext = dbContext;
        _projectCompletionRequestRepository = projectCompletionRequestRepository;
    }

    public async Task<IActionResult> PutCompletionRequestsAsync(Guid projectId)
    {
        var project = await _dbContext.Projects
            .Where(p => p.Id == projectId).FirstOrDefaultAsync();
        if (project == null)
        {
            return new NotFoundObjectResult("Project not found.");
        }
        
        var completionRequests = await _dbContext.RequestForProjectCompletions
            .Where(r => r.ProjectId == projectId).FirstOrDefaultAsync();
        
        if (completionRequests != null && completionRequests.RequestStatus != ProjectRequestStatus.REJECTED.ToString())
        {
            return new BadRequestObjectResult("Request for project completion already exists.");
        }

        var request = await _projectCompletionRequestRepository.PutCompletionRequestsAsync(projectId);

        return request;

    }

    public async Task<IActionResult> GetCompletionRequestsAsync(Guid Id)
    {
        var user = await _dbContext.Users
            .Include(u => u.Students)
            .Include(u => u.IndustryExperts)
            .Where(u => (u.Students.Any(s => s.Id == Id) || u.IndustryExperts.Any(i => i.Id == Id)))
            .FirstOrDefaultAsync();

        var project = await _dbContext.Projects
            .Include(r => r.requestForProjectCompletions)
            .Where(p => (p.StudentId == Id || p.IndExpertId == Id) && (!p.requestForProjectCompletions.Any())).FirstOrDefaultAsync();
        
        if (user == null)
        {
            return new NotFoundObjectResult("User does not exist with this id");
        }

        if (project == null)
        {
            return new NotFoundObjectResult("No Projects Found for this user.");
        }

        return await _projectCompletionRequestRepository.GetCompletionRequestsAsync(Id);

    }

    public async Task<IActionResult> GetCompletionRequestForProjectAsync(Guid Id)
    {
        var user = await _dbContext.Users
            .Include(u => u.Students)
            .Include(u => u.IndustryExperts)
            .Where(u => (u.Students.Any(s => s.Id == Id) || u.IndustryExperts.Any(i => i.Id == Id) || u.Students.Any(s => s.Projects.Any(p => p.Id == Id)) || u.IndustryExperts.Any(i => i.Projects.Any(p => p.Id == Id))))
            .FirstOrDefaultAsync();
        
        var project = await _dbContext.Projects
            .Include(r => r.requestForProjectCompletions)
            .Where(p => p.Id == Id).FirstOrDefaultAsync();
        
        if (user == null)
        {
            return new NotFoundObjectResult("User does not exist with this id");
        }

        if (project == null)
        {
            return new NotFoundObjectResult("No Projects Found for this user.");
        }

        return await _projectCompletionRequestRepository.GetCompletionRequestForProjectAsync(Id);
    }

    public async Task<IActionResult> HandleRequestAsync(Guid RequestId, string status)
    {
        if (status != ProjectRequestStatus.ACCEPTED.ToString() &&
            status != ProjectRequestStatus.REJECTED.ToString())
        {
            return new BadRequestObjectResult("Invalid status. It should only be ACCEPTED or REJECTED.");
        }

        var result = await _projectCompletionRequestRepository.HandleRequestAsync(RequestId, status);

        if (result == null)
            return new BadRequestObjectResult("Request not found or already accepted.");

        return new OkObjectResult($"Request status updated to {status}");
    }
}