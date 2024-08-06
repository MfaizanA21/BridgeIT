using BridgeITAPIs.Models;
using BridgeITAPIs.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

    [HttpPost("projects")]
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
}
