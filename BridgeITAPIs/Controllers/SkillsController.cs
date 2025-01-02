using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BridgeITAPIs.DTOs.SkillsDTOs;
using BridgeITAPIs.Models;

namespace BridgeITAPIs.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SkillsController : ControllerBase
{
    private readonly BridgeItContext _dbContext;

    public SkillsController(BridgeItContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("post-skills")]
    public async Task<IActionResult> AddSkills([FromBody] string skill)
    {
        if (skill == null)
        {
            return BadRequest("Skills Data is null.");
        }

        var skills = new Skill
        {
            Id = Guid.NewGuid(),
            Skill1 = skill
        };
        
        await _dbContext.Set<Skill>().AddAsync(skills);
        await _dbContext.SaveChangesAsync();
        return Ok("Skill saved Successfully!");
    }

    [HttpGet("get-skills")]
    public async Task<IActionResult> GetSkills()
    {
        var skills = await _dbContext.Skills.ToListAsync();

        if (skills == null)
        {
            return NotFound("Skills not found.");
        }

        var dtoList = skills.Select(s => new SkillsDTO
        {
            Id = s.Id,
            Skill = s.Skill1 ?? string.Empty
        }).ToList();

        return Ok(dtoList);

    }

    [HttpGet("get-skill-name/{name}")]
    public async Task<IActionResult> GetSkillsByName(string name)
    {
        var skills = await _dbContext.Skills
            .Where(s => s.Skill1 != null && s.Skill1.ToLower().Contains(name.ToLower()))
            .ToListAsync();

        if (skills == null)
        {
            return NotFound("Skills not found.");
        }

        var dtoList = skills.Select(s => new SkillsDTO
        {
            Id = s.Id,
            Skill = s.Skill1 ?? string.Empty
        }).ToList();

        return Ok(dtoList);
    }

    [HttpGet("get-skills-by-id/{userId}")]
    public async Task<IActionResult> GetSkillsById(Guid userId)
    {
        var student = await _dbContext.Students
            .FirstOrDefaultAsync(s => s.User.Id == userId);

        if (student?.skills == null)
        {
            return BadRequest("No Student Available against this id");
        }

        List<string> skills = new List<string>();

        if (student.skills != null)
        {
            skills = student.skills.Split(',').ToList();
        }

        return Ok(skills);
    }
}
