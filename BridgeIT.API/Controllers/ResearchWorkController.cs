using BridgeIT.API.DTOs.ResearchPaperDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BridgeIT.Domain.Models;
using BridgeIT.Infrastructure;

namespace BridgeIT.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ResearchWorkController : ControllerBase
{
    private readonly BridgeItContext _dbContext;

    public ResearchWorkController(BridgeItContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("add-researchpaper")]
    public async Task<IActionResult> AddResearchPaper([FromBody] RegisterResearchDTO dto)
    {

        if (dto.facultyId == Guid.Empty)
        {
            return BadRequest ("Faculty id can never be null");
        }

        if(dto.paperName == String.Empty || dto.category == String.Empty || dto.publishChannel == string.Empty)
        {
            return BadRequest("Name, Category or publishChannel is required.");
        }

        var paper = new ResearchWork
        {
            Id = Guid.NewGuid(),
            PaperName = dto.paperName,
            Category = dto.category,
            PublishChannel = dto.publishChannel,
            Link = dto.link,
            OtherResearchers = dto.otherResearchers,
            YearOfPublish = dto.yearOfPublish,
            FacultyId = dto.facultyId
        };

        await _dbContext.ResearchWorks.AddAsync(paper);
        await _dbContext.SaveChangesAsync();
        return Ok("Research Paper Added Successfully!");
    }

    [HttpGet("get-researchpapers")]
    public async Task<IActionResult> GetResearchPaper()
    {
        var paper = await _dbContext.ResearchWorks
            .Include(p => p.Faculty)
            .ThenInclude(f => f!.User)
            .ToListAsync();

        if (!paper.Any())
        {
            return NotFound("No Research Papers Found.");
        }

        var list = paper.Select(p => new GetResearchPaperDTO
        {
            Id = p.Id,
            PaperName = p.PaperName ?? string.Empty,
            Category = p.Category ?? string.Empty,
            PublishChannel = p.PublishChannel ?? string.Empty,
            Link = p.Link ?? string.Empty,
            OtherResearchers = p.OtherResearchers ?? string.Empty,
            YearOfPublish = p.YearOfPublish,
            FacultyId = p.FacultyId,
            FacultyName = p.Faculty?.User!.FirstName ?? string.Empty
        }).ToList();

        return Ok(list);
        
     }

    [HttpGet("get-researchwork-by-id/{Id}")]
    public async Task<IActionResult> GetResearchWorkById(Guid Id)
    {
        var papers = await _dbContext.ResearchWorks
            .Include(p => p.Faculty)
            .Where(p => p.FacultyId == Id)
            .ToListAsync();

        if (!papers.Any())
        {
            return NotFound("No Research Paper for this Faculty");
        }

        var list = papers.Select(p => new GetResearchPaperDTO
        {
            Id = p.Id,
            FacultyId = p.FacultyId,
            PaperName = p.PaperName ?? string.Empty,
            Category = p.Category ?? string.Empty,
            Link = p.Link ?? string.Empty,
            PublishChannel = p.PublishChannel ?? string.Empty,
            OtherResearchers = p.OtherResearchers ?? string.Empty,
            YearOfPublish = p.YearOfPublish,

        }).ToList();

        return Ok(list);
    }

    [HttpDelete("delete-researchpaper/{Id}")]
    public async Task<IActionResult> DeleteResearchPaper(Guid Id)
    {
        var paper = await _dbContext.ResearchWorks.FindAsync(Id);
        if (paper == null)
        {
            return NotFound("Research Paper not found.");
        }

        _dbContext.ResearchWorks.Remove(paper);
        await _dbContext.SaveChangesAsync();
        return Ok("Research Paper Deleted Successfully.");
    }
}
