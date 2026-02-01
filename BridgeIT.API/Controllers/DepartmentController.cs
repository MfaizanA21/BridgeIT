using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BridgeIT.API.DTOs.UniversityDTOs;
using BridgeIT.Domain.Models;
using BridgeIT.Infrastructure;

namespace BridgeIT.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController : ControllerBase
{
    private readonly BridgeItContext _dbContext;

    public DepartmentController(BridgeItContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("add-department")]
    public async Task<IActionResult> AddDepartment([FromBody] string departmentName)
    {
        if (departmentName == String.Empty)
        {
            return BadRequest("Department name is null.");
        }

        var department = new Department
        {
            Id = Guid.NewGuid(),
            Department1 = departmentName
        };
        await _dbContext.Set<Department>().AddAsync(department);
        await _dbContext.SaveChangesAsync();
        return Ok(department);
    }

    [HttpDelete("delete-department/{departmentId}")]
    public async Task<IActionResult> DeleteDepartment(Guid departmentId)
    {
        var department = await _dbContext.Departments
            .FirstOrDefaultAsync(d => d.Id == departmentId);

        if (department == null)
        {
            return NotFound("Department not found.");
        }

        _dbContext.Departments.Remove(department);
        await _dbContext.SaveChangesAsync();

        return Ok("Department deleted successfully.");
    }

    [HttpGet("get-departments")]
    public async Task<IActionResult> GetAllDepartments()
    {
        var departments = await _dbContext.Departments.ToListAsync();

        if (!departments.Any())
        {
            return NotFound("Departments not found.");
        }

        var list = departments.Select(d => new DepartmentDTO
        {
            Id = d.Id,
            department = d.Department1 ?? string.Empty
        }).ToList();
        return Ok(list);
    }
}
