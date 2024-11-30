using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BridgeITAPIs.DTOs.MilestoneDTOs;
using BridgeITAPIs.Models;
using Microsoft.IdentityModel.Tokens;

namespace BridgeITAPIs.Controllers;

[Route("api/milestone")]
[ApiController]
public class MilestoneController : ControllerBase
{
    private readonly BridgeItContext _dbContext;
    
    public MilestoneController(BridgeItContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("add-milestone/{projectId}")]
    public async Task<IActionResult> AddMilestone(Guid projectId, [FromBody] PostMilestoneDTO dto)
    {
        var project = await _dbContext.Projects
            .FirstOrDefaultAsync(p => p.Id == projectId);

        if (project == null)
        {
            return BadRequest("Project not found.");
        }

        if (project.IndExpertId == null)
        {
            return BadRequest("Only industry projects can have milestones.");
        }
        
        var milestone = new MileStone
        {
            Id = Guid.NewGuid(),
            ProjectId = projectId,
            Title = dto.Title,
            Description = dto.Description,
            AchievementDate = dto.AchievementDate,
        };
        
        _dbContext.MileStones.Add(milestone);
        await _dbContext.SaveChangesAsync();
        
        return Ok("Milestone added successfully.");
    }

    [HttpGet("get-project-milestones/{projectId}")]
    public async Task<IActionResult> GetProjectMilestones(Guid projectId)
    {
        var milestones = await _dbContext.MileStones
            .Where(m => m.ProjectId == projectId)
            .ToListAsync();
        
        if (milestones.IsNullOrEmpty())
        {
            return NotFound("Milestones not found.");
        }
        
        var dtos = milestones.Select(m => new GetMilestoneDTO
        {
            Id = m.Id,
            Title = m.Title ?? String.Empty,
            Description = m.Description ?? String.Empty,
            AchievementDate = m.AchievementDate,
            ProjectId = m.ProjectId,
        }).ToList();
        
        return Ok(dtos);
    }
    
}