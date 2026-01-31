using BridgeITAPIs.DTOs.ProjectModuleDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BridgeITAPIs.Controllers;

[ApiController]
[Route("api/project-module")]
public class ProjectModuleController : Controller
{
    private readonly BridgeItContext _dbContext;

    public ProjectModuleController(BridgeItContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("add/{projectId}")]
    public async Task<IActionResult> AddModule(Guid projectId, [FromBody] AddProjectModuleDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var projectExists = await _dbContext.Projects.AnyAsync(p => p.Id == projectId);
        if (!projectExists)
        {
            return NotFound($"Project with ID {projectId} not found.");
        }

        var module = new ProjectModule
        {
            Id = Guid.NewGuid(),
            ProjectId = projectId,
            Name = dto.Name,
            Description = dto.Description,
            Status = false
        };
        
        _dbContext.ProjectModules.Add(module);
        await _dbContext.SaveChangesAsync();

        return Ok(new { message = "Module added successfully", moduleId = module.Id });

    }

    [HttpGet("get-all/{projectId}")]
    public async Task<IActionResult> GetModules(Guid projectId)
    {
        var modules = await _dbContext.ProjectModules
            .Include(m => m.Project)
            .Where(m => m.ProjectId == projectId)
            .ToListAsync();

        if (!modules.Any())
        {
            return NotFound($"No modules found for project with ID {projectId}.");
        }
        
        var dtoList = modules.Select(m => new GetProjectModuleDTO
        {
            Id = m.Id,
            Name = m.Name,
            Description = m.Description,
            Status = m.Status,
            ProjectId = m.ProjectId,
            ProjectName = m.Project!.Title!
        }).ToList();
        
        return Ok(dtoList);
    }
    
    [HttpPatch("update-status/{moduleId}")]
    public async Task<IActionResult> UpdateModuleStatus(Guid moduleId, [FromQuery] bool status)
    {
        var module = await _dbContext.ProjectModules.FindAsync(moduleId);

        if (module == null)
        {
            return NotFound($"Project module with ID {moduleId} not found.");
        }

        if (module.Status == status)
        {
            return BadRequest("Module status is of the same status.");
        }

        module.Status = status;

        _dbContext.ProjectModules.Update(module);
        await _dbContext.SaveChangesAsync();

        return Ok(new { message = "Module status updated successfully", moduleId = module.Id, newStatus = module.Status });
    }
    
    [HttpGet("{moduleId}")]
    public async Task<IActionResult> GetModuleById(Guid moduleId)
    {
        var module = await _dbContext.ProjectModules
            .Include(m => m.Project)
            .FirstOrDefaultAsync(m => m.Id == moduleId);

        if (module == null)
        {
            return NotFound($"Module with ID {moduleId} not found.");
        }

        var dto = new GetProjectModuleDTO
        {
            Id = module.Id,
            Name = module.Name,
            Description = module.Description,
            Status = module.Status,
            ProjectId = module.ProjectId,
            ProjectName = module.Project?.Title ?? string.Empty
        };

        return Ok(dto);
    }

}