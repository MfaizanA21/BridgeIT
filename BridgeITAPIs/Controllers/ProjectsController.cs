using BridgeITAPIs.Models;
using BridgeITAPIs.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BridgeITAPIs.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly BridgeItContext _dbContext;

    public ProjectsController(BridgeItContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("create-projects")]
    public async Task<IActionResult> AddProject([FromBody] AddProjectDTO dto)
    {
        if (dto == null)
        {
            return BadRequest("Project Data is null.");
        }

        if (dto.StudentId == null && dto.IndExpertId == null)
        {
            return BadRequest("Either StudentId or IndExpertId must be provided");
        }

        var project = new Project
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Description = dto.Description,
            Team = dto.Team,
            Stack = dto.Stack,
            CurrentStatus = dto.CurrentStatus,
            StartDate = DateOnly.Parse(dto.StartDate),
            EndDate = DateOnly.Parse(dto.EndDate),
            StudentId = dto.StudentId,
            IndExpertId = dto.IndExpertId
        };

        await _dbContext.Projects.AddAsync(project);
        await _dbContext.SaveChangesAsync();

        return Ok("Project added successfully.");
    }

    [HttpGet("get-all-projects")]
    public async Task<IActionResult> GetProjectTiles()
    {
        var projects = await _dbContext.Projects
            .Include(p => p.Student)
            .Include(p => p.IndExpert)
            .ToListAsync();

        if (projects == null)
        {
            return BadRequest("No projects found");
        }

        var projectDto = projects.Select(project => new ProjectTileDTO
        {
            Id = project.Id,
            Title = project?.Title ?? string.Empty,
            Description = project?.Description ?? string.Empty,
            Stack = project?.Stack ?? string.Empty,
            Status = project?.CurrentStatus ?? string.Empty,
            StudentId = project.StudentId,
            IndExpertId = project.IndExpertId,
            studentName = project.Student.User.FirstName ?? string.Empty,
            IndExpertName = project.IndExpert.User.FirstName ?? string.Empty

        }).ToList();

        return Ok(projectDto);

    }

    
}
