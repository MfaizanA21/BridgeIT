using BridgeITAPIs.DTOs.FacultyDTOs;
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
            Id = faculty.Id,
            UserId = faculty.UserId,
            uniId = faculty.UniId,
            FirstName = faculty.User?.FirstName ?? string.Empty,
            LastName = faculty.User?.LastName ?? string.Empty,
            Email = faculty.User?.Email ?? string.Empty,
            ImageData = faculty.User?.ImageData,
            Description = faculty.User?.description ?? string.Empty,
            Department = faculty.Department ?? string.Empty,
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
                Id = f.Id,
                UserId = f.UserId,
                uniId = f.UniId,
                FirstName = f.User?.FirstName ?? string.Empty,
                LastName = f.User?.LastName ?? string.Empty,
                Email = f.User?.Email ?? string.Empty,
                ImageData = f.User?.ImageData,
                Description = f.User?.description ?? string.Empty,
                Department = f.Department ?? string.Empty,
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
            .Where(f => f.Uni != null && f.Uni.Name.ToLower().Contains(uniName.ToLower()))
            .ToListAsync();

        if (faculties == null)
        {
            return NotFound("Faculty not found.");
        }

        var dtoList = faculties.Select(f => new GetFacultyDTO
        {
            Id = f.Id,
            UserId = f.UserId,
            uniId = f.UniId,
            FirstName = f.User?.FirstName ?? string.Empty,
            LastName = f.User?.LastName ?? string.Empty,
            Email = f.User?.Email ?? string.Empty,
            ImageData = f.User?.ImageData,
            Description = f.User?.description ?? string.Empty,
            Department = f.Department ?? string.Empty,
            Interest = f.Interest != null ? new List<string> { f.Interest } : new List<string>(),
            Post = f.Post ?? string.Empty,
            UniversityName = f.Uni?.Name ?? string.Empty,
            Address = f.Uni?.Address ?? string.Empty
        })
        .ToList();

        return Ok(dtoList);
    }

    [HttpGet("faculty-by-name/{name}")]
    public async Task<IActionResult> GetFacultyByName(string name)
    {
        var faculty = await _dbContext.Faculties
            .Include(f => f.User)
            .Include(f => f.Uni)
            .Where(f => f.User != null &&
                    (f.User.FirstName != null && f.User.FirstName.ToLower().Contains(name.ToLower()) ||
                     f.User.LastName != null && f.User.LastName.ToLower().Contains(name.ToLower())))
            .ToListAsync();

        if (faculty == null || !faculty.Any())
        {
            return BadRequest("User Not found");
        }

        var dtoList = faculty.Select(f => new GetFacultyDTO
        {
            Id = f.Id,
            UserId = f.UserId,
            uniId = f.UniId,
            FirstName = f.User?.FirstName ?? string.Empty,
            LastName = f.User?.LastName ?? string.Empty,
            Email = f.User?.Email ?? string.Empty,
            ImageData = f.User?.ImageData,
            Description = f.User?.description ?? string.Empty,
            Department = f.Department ?? string.Empty,
            Interest = f.Interest != null ? new List<string> { f.Interest } : new List<string>(),
            Post = f.Post ?? string.Empty,
            UniversityName = f.Uni?.Name ?? string.Empty,
            Address = f.Uni?.Address ?? string.Empty
        })
            .ToList();

        return Ok(dtoList);

    }


}
