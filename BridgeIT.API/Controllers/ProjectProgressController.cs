using BridgeITAPIs.DTOs.ProjectProgressDTOs;
using BridgeITAPIs.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BridgeITAPIs.Controllers;

[ApiController]
[Route("api/project-progress")]
public class ProjectProgressController : Controller
{
    private readonly BridgeItContext _dbContext;
    
    public ProjectProgressController(BridgeItContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("add-tasks/{projectId}")]
    public async Task<IActionResult> AddTasks(Guid projectId, [FromBody] AddProjectProgressDTO addProjectProgressDto)
    {
        var project = await _dbContext.Projects
            .FirstOrDefaultAsync(p => p.Id == projectId);
        
        if (project == null)
        {
            return NotFound("Prject Not Found");
        }

        var projectProgress = new ProjectProgress{
            Task = addProjectProgressDto.Task,
            Description = addProjectProgressDto.Description,
            ProjectId = projectId,
            Id = Guid.NewGuid(),
            TaskStatus = ProjectProgressStatus.PENDING.ToString()
        };
        
        await _dbContext.ProjectProgresses.AddAsync(projectProgress);
        await _dbContext.SaveChangesAsync();
        
        return Ok("Task Added Successfully");
    }
    
    [HttpGet("get-tasks/{projectId}")]
    public async Task<IActionResult> GetTasks(Guid projectId)
    {
        var projectProgress = await _dbContext.ProjectProgresses
            .Include(p => p.Project)
            .Where(p => p.ProjectId == projectId)
            .ToListAsync();

        if (!projectProgress.Any())
        {
            return BadRequest("No Tasks Found");
        }
        
        var projectProgressDTOs = projectProgress.Select(p => new GetProjectProgressDTO
        {
            Id = p.Id,
            ProjectId = p.ProjectId,
            Description = p.Description,
            Task = p.Task,
            TaskStatus = p.TaskStatus
        });

        return Ok(projectProgressDTOs);
    }
    
    [HttpPut("marks-as-complete/{projectId}/{taskId}")]
    public async Task<IActionResult> UpdateTaskStatus(Guid projectId, Guid taskId)
    {
        var projectProgress = await _dbContext.ProjectProgresses
            .FirstOrDefaultAsync(p => p.ProjectId == projectId && p.Id == taskId);

        if (projectProgress == null)
        {
            return NotFound("Task Not Found");
        }

        projectProgress.TaskStatus = ProjectProgressStatus.COMPLETED.ToString();
        await _dbContext.SaveChangesAsync();
        
        return Ok("Task Status Updated Successfully");
    }
}