using Microsoft.AspNetCore.Mvc;
using BridgeITAPIs.DTOs.IdeaDTOs;

namespace BridgeITAPIs.Controllers;

[Route("api/ideas")]
[ApiController]
public class IdeaController : Controller
{
    private readonly BridgeItContext _dbContext;

    public IdeaController(BridgeItContext dbContext)
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
}