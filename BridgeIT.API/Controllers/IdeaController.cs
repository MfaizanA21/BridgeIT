using Microsoft.AspNetCore.Mvc;
using BridgeIT.API.DTOs.IdeaDTOs;
using Microsoft.EntityFrameworkCore;
using BridgeIT.API.services.Implementation;
using BridgeIT.Domain.Models;
using BridgeIT.Infrastructure;

namespace BridgeIT.API.Controllers;

[Route("api/ideas")]
[ApiController]
public class IdeaController : Controller
{
    private readonly BridgeItContext _dbContext;
    
    public IdeaController(BridgeItContext dbContext, MailService mailService)
    {
        _dbContext = dbContext;
    }

    [HttpPost("add-idea/{facultyId}")]
    public async Task<IActionResult> AddIdea(Guid facultyId, [FromBody] AddIdeaDto dto)
    {
        
        await _dbContext.Ideas.AddAsync(new Idea
        {
            Id = Guid.NewGuid(),
            Name = dto.Title,
            Technology = dto.Technology,
            Description = dto.Description,
            FacultyId = facultyId
        });

        await _dbContext.SaveChangesAsync();
        
        return Ok("Idea added successfully.");
    }

    [HttpGet("get-ideas-by-uni/{uniId}")]
    public async Task<IActionResult> GetIdeaByUni(Guid uniId)
    {
        var ideas = await _dbContext.Ideas
            .Include(f => f.Faculty)
            .ThenInclude(u => u!.Uni)
            .Include(f => f.Faculty)
            .ThenInclude(u => u!.User)
            .Where(u => u.Faculty.Uni.Id == uniId)
            .ToListAsync();

        if (!ideas.Any())
        {
            return BadRequest("No ideas found.");
        }

        var dto = ideas.Select(idea => new GetIdeasDto
        {
            Id = idea.Id,
            Title = idea.Name,
            Technology = idea.Technology,
            Description = idea.Description,
            FacultyId = idea.FacultyId,
            FacultyName = idea.Faculty.User.FirstName + " " + idea.Faculty.User.LastName,
            Email = idea.Faculty.User.Email,
            UniId = idea.Faculty.Uni.Id,
            UserId = idea.Faculty.UserId,
            UniName = idea.Faculty.Uni.Name
        }).ToList();
        
        return Ok(dto); 
    }
    
    [HttpGet("get-ideas-by-faculty-id/{Id}")]
    public async Task<IActionResult> GetIdeaByFacultyId(Guid Id)
    {
        var ideas = await _dbContext.Ideas
            .Include(f => f.Faculty)
            .ThenInclude(u => u!.Uni)
            .Include(f => f.Faculty)
            .ThenInclude(u => u!.User)
            .Where(u => u.Faculty!.Id == Id || u.Faculty.UserId == Id)
            .ToListAsync();

        if (!ideas.Any())
        {
            return BadRequest("No ideas found.");
        }

        var dto = ideas.Select(idea => new GetIdeasDto
        {
            Id = idea.Id,
            Title = idea.Name,
            Technology = idea.Technology,
            Description = idea.Description,
            FacultyId = idea.FacultyId,
            FacultyName = idea.Faculty.User.FirstName + " " + idea.Faculty.User.LastName,
            Email = idea.Faculty.User.Email,
            UniId = idea.Faculty.Uni!.Id,
            UserId = idea.Faculty.UserId,
            UniName = idea.Faculty.Uni.Name
        }).ToList();
        
        return Ok(dto); 
    }

    [HttpGet("get-idea-by-id/{Id}")]
    public async Task<IActionResult> GetIdeaById(Guid Id)
    {
        var ideas = await _dbContext.Ideas
            .Include(f => f.Faculty)
            .ThenInclude(u => u!.Uni)
            .Include(f => f.Faculty)
            .ThenInclude(u => u!.User)
            .Where(u => u.Id == Id)
            .ToListAsync();

        if (!ideas.Any())
        {
            return BadRequest("No ideas found.");
        }

        var dto = ideas.Select(idea => new GetIdeasDto
        {
            Id = idea.Id,
            Title = idea.Name,
            Technology = idea.Technology,
            Description = idea.Description,
            FacultyId = idea.FacultyId,
            FacultyName = idea.Faculty.User.FirstName + " " + idea.Faculty.User.LastName,
            Email = idea.Faculty.User.Email,
            UniId = idea.Faculty.Uni.Id,
            UserId = idea.Faculty.UserId,
            UniName = idea.Faculty.Uni.Name
        }).ToList();
        
        return Ok(dto); 
    }

}