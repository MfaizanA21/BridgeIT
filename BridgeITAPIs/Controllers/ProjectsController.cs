using BridgeITAPIs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BridgeITAPIs.DTOs.ProjectDTOs;

namespace BridgeITAPIs.Controllers;

[Route("api/projects")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly BridgeItContext _dbContext;

    public ProjectsController(BridgeItContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("student-add-projects")]
    public async Task<IActionResult> AddProject([FromBody] StudentAddProjectDTO dto)
    {
        if (dto == null)
        {
            return BadRequest("Project Data is null.");
        }

        if (dto.StudentId == null)
        {
            return BadRequest("Either StudentId must be provided");
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
        };

        await _dbContext.Projects.AddAsync(project);
        await _dbContext.SaveChangesAsync();

        return Ok("Project added successfully.");
    }

    [HttpGet("get-student-projects")]
    public async Task<IActionResult> GetProjectTiles()
    {
        var projects = await _dbContext.Projects
            .Include(p => p.Student)
                .ThenInclude(s => s.User)
            .Include(p => p.IndExpert)
            .Where(p => p.StudentId != null)
            .ToListAsync();

        if (projects == null)
        {
            return BadRequest("No projects found");
        }

        var projectDto = projects.Select(project => new StudentProjectTileDTO
        {
            Id = project.Id,
            Title = project?.Title ?? string.Empty,
            Description = project?.Description ?? string.Empty,
            Stack = project?.Stack ?? string.Empty,
            Status = project?.CurrentStatus ?? string.Empty,
            StudentId = project?.StudentId,
            studentName = project?.Student?.User?.FirstName ?? string.Empty,
        }).ToList();

        return Ok(projectDto);

    }

    [HttpPost("expert-post-project")]
    public async Task<IActionResult> PostProject([FromBody] IndExptAddProjectDTO dto)
    {
        if (dto == null)
        {
            return BadRequest("Project Data is null.");
        }

        if (dto.IndExpertId == null)
        {
            return BadRequest("IndExpertId must be provided");
        }

        var project = new Project
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Description = dto.Description,
            /*Team = dto.Team,
            Stack = dto.Stack,
            CurrentStatus = dto.CurrentStatus,
            StartDate = DateOnly.Parse(dto.StartDate),*/
            EndDate = DateOnly.Parse(dto.EndDate),
            IndExpertId = dto.IndExpertId,
        };

        await _dbContext.Projects.AddAsync(project);
        await _dbContext.SaveChangesAsync();

        return Ok("Project added successfully.");
    }

    [HttpGet("get-expert-projects")]
    public async Task<IActionResult> GetExpertProjects()
    {
        var projects = await _dbContext.Projects
            .Include(p => p.IndExpert)
            .Where(p => p.IndExpertId != null)
            .ToListAsync();

        if (projects == null)
        {
            return BadRequest("No projects found");
        }

        var projectDto = projects.Select(project => new IndExptProjectTileDTO
        {
            Id = project.Id,
            Title = project?.Title ?? string.Empty,
            Description = project?.Description ?? string.Empty,
            EndDate = project?.EndDate.ToString() ?? string.Empty,
            IndExpertId = project?.IndExpertId,
        }).ToList();

        return Ok(projectDto);
    }

    [HttpGet("get-student-projects-by-id/{id}")]
    public async Task<IActionResult> GetStudentProjectsById(Guid id)
    {
        var projects = await _dbContext.Projects
            .Include(p => p.Student)
                .ThenInclude(s => s.User)
            .Include(p => p.IndExpert)
            .Where(p => p.StudentId == id)
            .ToListAsync();

        if (projects == null)
        {
            return BadRequest("No projects found");
        }

        var projectDto = projects.Select(project => new StudentProjectTileDTO
        {
            Id = project.Id,
            Title = project?.Title ?? string.Empty,
            Description = project?.Description ?? string.Empty,
            Stack = project?.Stack ?? string.Empty,
            Status = project?.CurrentStatus ?? string.Empty,
            StudentId = project?.StudentId,
            studentName = project?.Student?.User?.FirstName + " " + project?.Student?.User?.LastName ?? string.Empty,
        }).ToList();

        return Ok(projectDto);
    }

    [HttpGet("get-expert-projects-by-id/{Expertid}")]
    public async Task<IActionResult> GetExpertProjectsById(Guid Expertid)
    {
        var projects = await _dbContext.Projects
            .Include(i => i.IndExpert)
                .ThenInclude(u => u.User)
            .Where(p => p.IndExpertId == Expertid).ToListAsync();

        if (projects == null)
        {
            return BadRequest("No Projects Found");        
        }

        var list = projects.Select(project => new IndExptProjectTileDTO
        {
            Id = project.Id,
            Title = project?.Title ?? string.Empty,
            Description = project?.Description ?? string.Empty,
            IndExpertId = project?.IndExpertId,
            EndDate = project?.EndDate.ToString(),
            Name = project?.IndExpert?.User?.FirstName +" " + project?.IndExpert?.User?.LastName ?? string.Empty,

        }).ToList();

        return Ok(list);
    }
}
