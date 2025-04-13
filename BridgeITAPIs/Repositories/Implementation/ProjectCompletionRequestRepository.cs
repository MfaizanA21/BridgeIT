using BridgeITAPIs.Repositories.interfaces;
using Microsoft.AspNetCore.Mvc;
using BridgeITAPIs.DTOs.ProjectCompletionRequestDTOs;
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
}