using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BridgeITAPIs.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BridgeITAPIs.Controllers;

[Route("api/get-student")]
[ApiController]
public class GetStudentController : ControllerBase
{
    private readonly BridgeItContext _dbContext;
    public GetStudentController(BridgeItContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("student-by-id/{userId}")]
    public async Task<IActionResult> GetStudent(Guid userId)
    {
        var student = await _dbContext.Students
            .Include(s => s.User)
            .Include(s => s.University)
            //.Include(s => s.Skills)
            .FirstOrDefaultAsync(s => s.UserId == userId);

        if (student == null)
        {
            return NotFound("Student not found.");
        }

        var dto = new GetStudentDTO
        {
            Id = student.Id,
            userId = student.UserId,
            universityId = student.UniversityId,
            FirstName = student.User?.FirstName ?? string.Empty,
            LastName = student.User?.LastName ?? string.Empty,
            Email = student.User?.Email ?? string.Empty,
            //Skills = student.Skills.Select(s => s.Skill1).ToList(),
            ImageData = student.User?.ImageData ?? Array.Empty<byte>(),
            UniversityName = student.University?.Name ?? string.Empty,
            Address = student.University?.Address ?? string.Empty,
            RollNumber = student?.RollNumber.ToString() ?? string.Empty
        };

        return Ok(dto);
    }

    [HttpGet("students")]
    public async Task<IActionResult> GetStudents()
    {
        var students = await _dbContext.Students
            .Include(s => s.User)
            .Include(s => s.University)
            //.Include(s => s.Skills)
            .ToListAsync();

        if (students == null)
        {
            return NotFound("Student not found.");
        }

        var dtoList = students.Select(s => new GetStudentDTO
        {
            Id = s.Id,
            userId = s.UserId,
            universityId = s.UniversityId,
            FirstName = s.User?.FirstName ?? string.Empty,
            LastName = s.User?.LastName ?? string.Empty,
            Email = s.User?.Email ?? string.Empty,
            Skills = s.Skills.Select(s => s.Skill1).ToList(),
            ImageData = s.User?.ImageData ?? Array.Empty<byte>(),
            //universityId = s.University?.Id ?? Guid.Empty,
            UniversityName = s.University?.Name ?? string.Empty,
            Address = s.University?.Address ?? string.Empty,
            RollNumber = s?.RollNumber.ToString() ?? string.Empty,
            //userId = s.User.Id,
            //Id = s.Id

        }).ToList();

        return Ok(dtoList);

    }

    [HttpGet("student-by-name/{name}")]
    public async Task<IActionResult> GetStudentsByName(string name)
    {
        var student = await _dbContext.Students
            .Include(s => s.User)
            .Include(s => s.University)
            //.Include(s => s.Skills)
            .Where(s => s.User != null &&
                    (s.User.FirstName != null && s.User.FirstName.ToLower().Contains(name.ToLower()) ||
                     s.User.LastName != null && s.User.LastName.ToLower().Contains(name.ToLower())))
            .ToListAsync();

        if (student == null || !student.Any())
        {
            return NotFound("Student not found.");
        }

        var dtoList = student.Select(s => new GetStudentDTO
        {
            Id = s.Id,
            userId = s.UserId,
            universityId = s.UniversityId,
            FirstName = s.User?.FirstName ?? string.Empty,
            LastName = s.User?.LastName ?? string.Empty,
            Email = s.User?.Email ?? string.Empty,
            //Skills = s.Skills.Select(s => s.Skill1).ToList(),
            ImageData = s.User?.ImageData ?? Array.Empty<byte>(),
            UniversityName = s.University?.Name ?? string.Empty,
            Address = s.University?.Address ?? string.Empty,
            RollNumber = s?.RollNumber.ToString() ?? string.Empty
        }).ToList();

        return Ok(dtoList);
    }

    [HttpGet("student-by-university/{uniName}")]
    public async Task<IActionResult> GetStudentsByUni(string uniName)
    {
        var students = await _dbContext.Students
            .Include(s => s.User)
            .Include(s => s.University)
            //.Include(s => s.Skills)
            .Where(s => s.University != null && s.University.Name == uniName)
            .ToListAsync();

        if (students == null)
        {
            return NotFound("Student not found.");
        }

        var dtoList = students.Select(s => new GetStudentDTO
        {
            FirstName = s.User?.FirstName ?? string.Empty,
            LastName = s.User?.LastName ?? string.Empty,
            Email = s.User?.Email ?? string.Empty,
            //Skills = s.Skills.Select(s => s.Skill1).ToList(),
            ImageData = s.User?.ImageData ?? Array.Empty<byte>(),
            UniversityName = s.University?.Name ?? string.Empty,
            Address = s.University?.Address ?? string.Empty,
            RollNumber = s?.RollNumber.ToString() ?? string.Empty
        }).ToList();

        return Ok(dtoList);
    }
    
}
