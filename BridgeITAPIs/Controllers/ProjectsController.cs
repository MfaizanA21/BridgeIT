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
            Link = dto.Link,
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

        if (!projects.Any())
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
            studentName = project?.Student?.User?.FirstName + " " + project?.Student?.User?.LastName,
            Link = project?.Link ?? string.Empty
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
            StartDate = DateOnly.Parse(dto.StartDate),*/
            Budget = dto.Budget,
            CurrentStatus = "Pending",
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
                .ThenInclude(p => p.User)
            .Where(p => p.IndExpertId != null && p.StudentId == null)
            .ToListAsync(); 

        if (projects == null)
        {
            return BadRequest("No projects found");
        }

        var projectDto = projects.Select(project => new IndExptProjectTileDTO
        {
            Id = project.Id,
            IndExpertId = project?.IndExpertId,
            Title = project?.Title ?? string.Empty,
            CurrentStatus = project?.CurrentStatus ?? string.Empty,
            Description = project?.Description ?? string.Empty,
            EndDate = project?.EndDate.ToString() ?? string.Empty,
            Budget = project?.Budget ?? 0,
            Name = project?.IndExpert?.User?.FirstName + " " + project?.IndExpert?.User?.LastName ?? string.Empty,
        }).ToList();

        return Ok(projectDto);
    }

    [HttpGet("get-student-with-expert-project-by-id/{studentId}")]
    public async Task<IActionResult> GetStudentWithExpertProjectById(Guid studentId)
    {
        var projects = await _dbContext.Projects
            .Include(p => p.Student)
                .ThenInclude(s => s.User)
            .Include(p => p.IndExpert)
                .ThenInclude(i => i.User)
            .Where(p => p.StudentId == studentId && p.IndExpertId != null)
            .ToListAsync();

        if (projects == null)
        {
            return BadRequest("No projects found");
        }

        var projectDto = projects.Select(project => new StudentWithExpertProjectDTO
        {
            Id = project.Id,
            StudentId = studentId,
            IndExpertId = project?.IndExpertId,
            Title = project?.Title ?? string.Empty,
            Description = project?.Description ?? string.Empty,
            Status = project?.CurrentStatus ?? string.Empty,
            EndDate = project?.EndDate.ToString() ?? string.Empty,
            Budget = project?.Budget ?? 0,
            studentName = project?.Student?.User?.FirstName + " " + project?.Student?.User?.LastName ?? string.Empty,
            expertName = project?.IndExpert?.User?.FirstName + " " + project?.IndExpert?.User?.LastName ?? string.Empty,
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
            .Where(p => p.StudentId == id && p.IndExpertId == null)
            .ToListAsync();

        if (!projects.Any())
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
            studentName = project?.Student?.User?.FirstName + " " + project?.Student?.User?.LastName,
            Link = project?.Link ?? string.Empty
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
            CurrentStatus = project?.CurrentStatus ?? string.Empty,
            Budget = project?.Budget ?? 0,
            Name = project?.IndExpert?.User?.FirstName +" " + project?.IndExpert?.User?.LastName ?? string.Empty,
            StudentId = project?.StudentId,

        }).ToList();

        return Ok(list);
    }

    [HttpGet("get-project-by-id/{id}")]
    public async Task<IActionResult> GetProjectById(Guid id)
    {
        var project = await _dbContext.Projects
            .Include(p => p.Student)
                .ThenInclude(s => s.User)
            .Include(p => p.IndExpert)
                .ThenInclude(i => i.User)
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();

        if (project == null)
        {
            return BadRequest("No project found");
        }

        var projectDto = new ProjectDTO
        {
            Id = project.Id,
            IndExpertId = project?.IndExpertId,
            StudentId = project?.StudentId,
            Title = project?.Title ?? string.Empty,
            Description = project?.Description ?? string.Empty,
            Stack = project?.Stack ?? string.Empty,
            Status = project?.CurrentStatus ?? string.Empty,
            StartDate = project?.StartDate.ToString() ?? string.Empty,
            EndDate = project?.EndDate.ToString() ?? string.Empty,
            studentName = project?.Student?.User?.FirstName + " " + project?.Student?.User?.LastName ?? string.Empty,
            expertName = project?.IndExpert?.User?.FirstName + " " + project?.IndExpert?.User?.LastName ?? string.Empty,
        };

        return Ok(projectDto);
    }

    [HttpGet("get-unassigned-expert-projects")]
    public async Task<IActionResult> GetUnassignedExpertProjects([FromQuery] Guid expertId)
    {
        var projects = await _dbContext.Projects
            .Include(i => i.IndExpert)
            .ThenInclude(u => u!.User)
            .Where(p => p.StudentId == null && p.IndExpertId == expertId)
            .ToListAsync();

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
            CurrentStatus = project?.CurrentStatus ?? string.Empty,
            Budget = project?.Budget ?? 0,
            Name = project?.IndExpert?.User?.FirstName + " " + project?.IndExpert?.User?.LastName,
        }).ToList();

        return Ok(list);
    }
    
    [HttpGet("get-assigned-expert-projects")]
    public async Task<IActionResult> GetAssignedExpertProjects([FromQuery] Guid expertId)
    {
        var projects = await _dbContext.Projects
            .Include(i => i.IndExpert)
            .ThenInclude(u => u!.User)
            .Where(p => p.StudentId != null && p.IndExpertId == expertId)
            .ToListAsync();

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
            CurrentStatus = project?.CurrentStatus ?? string.Empty,
            Budget = project?.Budget ?? 0,
            Name = project?.IndExpert?.User?.FirstName + " " + project?.IndExpert?.User?.LastName,
            StudentId = project!.StudentId
        }).ToList();

        return Ok(list);
    }
}
