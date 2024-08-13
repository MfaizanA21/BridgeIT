using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BridgeITAPIs.DTOs;
using BridgeITAPIs.Models;

namespace BridgeITAPIs.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GetUniversityController : ControllerBase
{
    private readonly BridgeItContext _dbContext;

    public GetUniversityController(BridgeItContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("university-by-id/{universityId}")]
    public async Task<IActionResult> GetUniversity(Guid universityId)
    {
        var university = await _dbContext.Universities
            .FirstOrDefaultAsync(u => u.Id == universityId);

        if (university == null)
        {
            return NotFound("University not found.");
        }

        var dto = new GetUniversityDTO
        {
            Id = university.Id,
            Name = university.Name,
            Address = university.Address,
            EstYear = university.EstYear
        };

        return Ok(dto);
    }

    [HttpGet("all-universities")]
    public async Task<IActionResult> GetUniversities()
    {
        var universities = await _dbContext.Universities.ToListAsync();

        if (universities == null)
        {
            return NotFound("Universities not found.");
        }

        var dtoList = universities.Select(u => new GetUniversityDTO
        {
            Id = u.Id,
            Name = u.Name,
            Address = u.Address,
            EstYear = u.EstYear
        }).ToList();

        return Ok(dtoList);
    }

    [HttpGet("university-by-name/{name}")]
    public async Task<IActionResult> GetUniversityByName(string name)
    {
        var university = await _dbContext.Universities
            .FirstOrDefaultAsync(u => u.Name.ToLower().Contains(name.ToLower()));

        if (university == null)
        {
            return NotFound("University not found.");
        }

        var dto = new GetUniversityDTO
        {
            Id = university.Id,
            Name = university.Name,
            Address = university.Address,
            EstYear = university.EstYear
        };

        return Ok(dto);
    }
}
