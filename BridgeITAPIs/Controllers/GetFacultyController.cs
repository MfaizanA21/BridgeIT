using BridgeITAPIs.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BridgeITAPIs.Controllers;

[Route("api/get-faculty")]
[ApiController]
public class GetFacultyController : ControllerBase
{
    private readonly BridgeItContext _dbContext;
    public GetFacultyController(BridgeItContext dbContext) {
        _dbContext = dbContext;
    }

    [HttpGet("faculty-by-id/{userId}")]
    public async Task<IActionResult> GetFaculty(Guid userId)
    {
        var faculty = await _dbContext.Faculties
            .Include(f => f.User)
            .Include(f => f.Uni)
            .FirstOrDefaultAsync(f => f.UserId == userId);

        if (faculty == null)
        {
            return NotFound("Faculty not found.");
        }

        var dto = new GetFacultyDTO
        {
            FirstName = faculty.User?.FirstName ?? string.Empty,
            LastName = faculty.User?.LastName ?? string.Empty,
            Email = faculty.User?.Email ?? string.Empty,
            ImageData = faculty.User?.ImageData ?? string.Empty,
            Interest = faculty.Interest != null ? new List<string> { faculty.Interest } : new List<string>(),
            Post = faculty.Post ?? string.Empty,
            UniversityName = faculty.Uni?.Name ?? string.Empty,
            Address = faculty.Uni?.Address ?? string.Empty
        };

        return Ok(dto);
    }

    [HttpGet("faculties")]
    public async Task<IActionResult> GetFaculties()
    {
        var faculties = await _dbContext.Faculties
            .Include(f => f.User)
            .Include(f => f.Uni)
            .ToListAsync();

        if (faculties == null)
        {
            return NotFound("Faculty not found.");
        }

            var dtoList = faculties.Select(f => new GetFacultyDTO
            {
                FirstName = f.User?.FirstName ?? string.Empty,
                LastName = f.User?.LastName ?? string.Empty,
                Email = f.User?.Email ?? string.Empty,
                ImageData = f.User?.ImageData ?? string.Empty,
                Interest = f.Interest != null ? new List<string> { f.Interest } : new List<string>(),
                Post = f.Post ?? string.Empty,
                UniversityName = f.Uni?.Name ?? string.Empty,
                Address = f.Uni?.Address ?? string.Empty
            })
            .ToList();

        return Ok(dtoList);
    }


    [HttpGet("faculty-by-university/{uniName}")]
    public async Task<IActionResult> GetFacultiesByUni(string uniName)
    {
        var faculties = await _dbContext.Faculties
            .Include(f => f.User)
            .Include(f => f.Uni)
            .Where(f => f.Uni != null && f.Uni.Name == uniName)
            .ToListAsync();

        if (faculties == null)
        {
            return NotFound("Faculty not found.");
        }

        var dtoList = faculties.Select(f => new GetFacultyDTO
        {
            FirstName = f.User?.FirstName ?? string.Empty,
            LastName = f.User?.LastName ?? string.Empty,
            Email = f.User?.Email ?? string.Empty,
            ImageData = f.User?.ImageData ?? string.Empty,
            Interest = f.Interest != null ? new List<string> { f.Interest } : new List<string>(),
            Post = f.Post ?? string.Empty,
            UniversityName = f.Uni?.Name ?? string.Empty,
            Address = f.Uni?.Address ?? string.Empty
        })
        .ToList();

        return Ok(dtoList);
    }
}
