using BridgeITAPIs.Repositories.interfaces;
using Microsoft.AspNetCore.Mvc;
using BridgeITAPIs.DTOs.ProjectCompletionRequestDTOs;
using BridgeITAPIs.Enums;
using Microsoft.EntityFrameworkCore;

namespace BridgeITAPIs.Repositories.Implementation;

public class ProjectCompletionRequestRepository: IProjectCompletionRequestsRepository
{
    private readonly BridgeItContext _dbContext;
    
    public ProjectCompletionRequestRepository(BridgeItContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IActionResult> PutCompletionRequestsAsync(Guid projectId)
    {
        var projectCompletionRequest = new RequestForProjectCompletion
        {
            id = Guid.NewGuid(),
            ProjectId = projectId,
        };
        
        await _dbContext.RequestForProjectCompletions.AddAsync(projectCompletionRequest);
        await _dbContext.SaveChangesAsync();
        return new OkObjectResult("Request for project completion has been sent successfully.");
    }

    public async Task<IActionResult> GetCompletionRequestsAsync(Guid Id)
    {
        var requests = await _dbContext.RequestForProjectCompletions
            .Include(r => r.project)
            .ThenInclude(s => s.Student)
            .ThenInclude(u => u.User)
            .Where(r => r.project.Student.Id == Id || r.project.IndExpertId == Id)
            .ToListAsync();
        
        var getRequests = requests.Select(r => new GetProjectCompletionRequestDto
        {
            Id = r.id,
            ProjectId = r.ProjectId,
            ProjectName = r.project.Title,
            ProjectDescription = r.project.Description,
            IndExpertId = r.project.IndExpertId,
            StudentId = r.project.StudentId,
            StudentName = r.project.Student?.User?.FirstName + " " + r.project.Student?.User?.LastName ?? "Unknown",
        }).ToList();

        return new OkObjectResult(getRequests);
    }

    public async Task<IActionResult> HandleRequestAsync(Guid requestId, string status)
    {
        var request = await _dbContext.RequestForProjectCompletions
            .FirstOrDefaultAsync(r => r.id == requestId);

        if (request == null || request.RequestStatus != ProjectRequestStatus.PENDING.ToString())
        {
            return new BadRequestObjectResult("Request not found or already accepted.");
        }

        request.RequestStatus = status;
        _dbContext.RequestForProjectCompletions.Update(request);
        await _dbContext.SaveChangesAsync();

        return new OkObjectResult($"Request status updated {status}");
    }
}