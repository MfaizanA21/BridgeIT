using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BridgeIT.API.DTOs.UniversityDTOs;
using BridgeIT.Infrastructure;
using BridgeIT.Domain.Models;

namespace BridgeIT.API.Controllers;

[Route("api/universities")]
[ApiController]
public class UniversitiesController : ControllerBase
{
    private readonly BridgeItContext _dbContext;

    public UniversitiesController(BridgeItContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("get-university-by-id/{universityId}")]
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
            Name = university.Name!,
            Address = university.Address!,
            EstYear = university.EstYear,
            uniImage = university.uniImage
        };

        return Ok(dto);
    }

    [HttpGet("get-all-universities")]
    public async Task<IActionResult> GetUniversities()
    {
        var universities = await _dbContext.Universities.ToListAsync();

        if (!universities.Any())
        {
            return NotFound("Universities not found.");
        }

        var dtoList = universities.Select(u => new GetUniversityDTO
        {
            Id = u.Id,
            Name = u.Name ?? String.Empty,
            Address = u.Address ?? String.Empty,
            EstYear = u.EstYear,
            uniImage = u.uniImage
        }).ToList();

        return Ok(dtoList);
    }

    [HttpGet("get-university-by-name/{name}")]
    public async Task<IActionResult> GetUniversityByName(string name)
    {
        var university = await _dbContext.Universities
            .FirstOrDefaultAsync(u => u.Name!.ToLower().Contains(name.ToLower()));

        if (university == null)
        {
            return NotFound("University not found.");
        }

        var dto = new GetUniversityDTO
        {
            Id = university.Id,
            Name = university.Name ?? String.Empty,
            Address = university.Address ?? String.Empty,
            EstYear = university.EstYear,
            uniImage = university.uniImage
        };

        return Ok(dto);
    }

    [HttpPost("add-university")]
    public async Task<IActionResult> AddUniversity([FromBody] AddUniversityDTO dto)
    {
        if (string.IsNullOrEmpty(dto.Name))
        {
            return BadRequest("University name is required.");
        }
        
        if(!string.IsNullOrEmpty(dto.uniImage))
        {
            byte[] ImageBytes = Convert.FromBase64String(dto.uniImage);

            if (ImageBytes.Length == 0)
            {
                return BadRequest("Invalid image data.");
            }
            else
            {
                var university = new University
                {
                    Id = Guid.NewGuid(),
                    Name = dto.Name,
                    Address = dto.Address,
                    EstYear = dto.EstYear,
                    uniImage = ImageBytes
                };
                await _dbContext.Universities.AddAsync(university);
                await _dbContext.SaveChangesAsync();
                return Ok("University added successfully.");
            }
        }
        else
        {
            return BadRequest("University image is required.");
        }


    }

    [HttpDelete("delete-university/{universityId}")]
    public async Task<IActionResult> DeleteUniversity(Guid universityId)
    {
        var university = await _dbContext.Universities
            .FirstOrDefaultAsync(u => u.Id == universityId);

        if (university == null)
        {
            return NotFound("University not found.");
        }

        _dbContext.Universities.Remove(university);
        await _dbContext.SaveChangesAsync();

        return Ok("University deleted successfully.");
    }

}
