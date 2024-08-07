using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BridgeITAPIs.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;


namespace BridgeITAPIs.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{

    private readonly BridgeItContext _dbContext;

    public StudentsController(BridgeItContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPatch("update-student/{Id}")]
    public async Task<IActionResult> UpdateStudent(Guid Id, [FromBody] EditStudentDTO dto)
    {
        var student = await _dbContext.Students
            .Include(s => s.User)
            .Include(s => s.University)
            .FirstOrDefaultAsync(s => s.Id == Id);

        if (student == null)
        {
            return NotFound("Student not found.");
        }

        if (student.User != null)
        {

            //student.User.Id = ;
            student.User.FirstName = dto.FirstName;
            student.User.LastName = dto.LastName;
            student.User.Email = dto.Email;
            student.User.ImageData = dto.ImageData;
        }

        if (student.University != null)
        {
            student.University.Id = dto.universityId.Value;
            //student.University.Name = dto.UniversityName;
            //student.University.Address = dto.Address;
        }

        student.RollNumber = int.Parse(dto.RollNumber); // Convert RollNumber to int

        await _dbContext.SaveChangesAsync();

        return Ok("Student updated successfully.");
    }


    [HttpPut("update-student/{Id}")]
    public async Task<IActionResult> EditStudent(Guid Id, [FromBody] EditStudentDTO dto)
    {
        var student = await _dbContext.Students
            .Include(s => s.User)
            .Include(s => s.University)
            .FirstOrDefaultAsync(s => s.Id == Id);

        if (student == null)
        {
            return NotFound("Student not found.");
        }

        if (student.User != null)
        {
            // Update User properties only if they are provided in the DTO
            if (!string.IsNullOrEmpty(dto.FirstName))
                student.User.FirstName = dto.FirstName;

            if (!string.IsNullOrEmpty(dto.LastName))
                student.User.LastName = dto.LastName;

            if (!string.IsNullOrEmpty(dto.Email))
                student.User.Email = dto.Email;

            if (!string.IsNullOrEmpty(dto.ImageData))
                student.User.ImageData = dto.ImageData;
        }

        if (student.University != null)
        {
            if (dto.universityId.HasValue)
                student.University.Id = dto.universityId.Value;
            //student.University.Name = dto.UniversityName;
            //student.University.Address = dto.Address;
        }

        if (!string.IsNullOrEmpty(dto.RollNumber))
        {
            student.RollNumber = int.Parse(dto.RollNumber); // Convert RollNumber to int
        }

        await _dbContext.SaveChangesAsync();

        return Ok("Student updated successfully.");
    }

    [HttpDelete("delete-students/{Id}")]
    public async Task<IActionResult> DeleteStudent(Guid Id)
    {
        var student = await _dbContext.Students
            .Include(s => s.User)
            .Include(s => s.University)
            .Include(s => s.Projects)
            .FirstOrDefaultAsync(s => s.Id == Id);

        if (student == null)
        {
            return NotFound("Student not found.");
        }

        if (student.Projects != null && student.Projects.Any())
        {
            // Optionally remove or update related projects
            _dbContext.Projects.RemoveRange(student.Projects);
        }

        if (student.User != null)
        {
            _dbContext.Users.Remove(student.User);
            //return NotFound("User not found.");
        }

        //await _dbContext.SaveChangesAsync();

        _dbContext.Students.Remove(student);
        await _dbContext.SaveChangesAsync();

        return Ok("Student deleted successfully.");
    }

}
