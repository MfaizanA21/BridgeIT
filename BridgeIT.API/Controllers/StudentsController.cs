using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BridgeIT.API.DTOs.StudentDTOs;
using BridgeIT.Infrastructure;


namespace BridgeIT.API.Controllers;

[Route("api/students")]
[ApiController]
public class StudentsController : ControllerBase
{

    private readonly BridgeItContext _dbContext;

    public StudentsController(BridgeItContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPatch("update-student-name/{Id}")]
    public async Task<IActionResult> UpdateStudentName(Guid Id, [FromBody] EditStudentNameRollnumberDTO dto)
    {
        var student = await _dbContext.Students
            .Include(s => s.User)
            //.Include(s => s.University)
            .FirstOrDefaultAsync(s => s.Id == Id);

        if (student == null)
        {
            return NotFound("Student not found.");
        }

        if (student.User != null )
        {
            if (!string.IsNullOrEmpty(dto.FirstName))
                student.User.FirstName = dto.FirstName;
            if (!string.IsNullOrEmpty(dto.LastName))
                student.User.LastName = dto.LastName;
        }

        if (!string.IsNullOrEmpty(dto.RollNumber))
        {
            student.RollNumber = int.Parse(dto.RollNumber); // Convert RollNumber to int
        }

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

            if (dto.ImageData.Length > 0)
                student.User.ImageData = dto.ImageData;
        }

        if (student.University != null)
        {
            if (dto.universityId.HasValue)
                student.University.Id = dto.universityId.Value;
        }

        if (!string.IsNullOrEmpty(dto.RollNumber))
        {
            student.RollNumber = int.Parse(dto.RollNumber); // Convert RollNumber to int
        }

        if (!string.IsNullOrEmpty(dto.CvLink))
        {
            student.cvLink = dto.CvLink;
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
            .ThenInclude(p => p.MileStones)
            .ThenInclude(m => m.MilestoneComments)
            .Include(s => s.Proposals)
            .FirstOrDefaultAsync(s => s.Id == Id);

        if (student == null)
        {
            return NotFound("Student not found.");
        }
        
        if (student.Proposals.Any())
        {
            _dbContext.Proposals.RemoveRange(student.Proposals);
        }

        if (student.Projects.Any())
        {
            foreach (var project in student.Projects)
            {
                foreach (var milestone in project.MileStones)
                {
                    if (milestone.MilestoneComments.Any())
                    {
                        _dbContext.MilestoneComments.RemoveRange(milestone.MilestoneComments);
                    }
                }

                if (project.MileStones.Any())
                {
                    _dbContext.MileStones.RemoveRange(project.MileStones);
                }
            }
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

    [HttpPut("add-cv/{Id}")]
    public async Task<IActionResult> StudentAddCV(Guid Id, [FromBody] string cvLink)
    {
        var student = await _dbContext.Students
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.Id == Id || s.UserId == Id);

        if (student == null)
        {
            return NotFound("No Student Found witht this id");
        }

        student.cvLink = cvLink;

        await _dbContext.SaveChangesAsync();

        return Ok("Cv Link Stored!");
    }

    [HttpGet("get-student-cv-by_id/{id}")]
    public async Task<IActionResult> GetStudentCvById(Guid id)
    {
        var student = await _dbContext.Students.FirstOrDefaultAsync(s => s.Id == id || s.UserId == id);
        
        if (student == null)
        {
            return NotFound("No Student Found with this id");
        }

        if (student.cvLink == null)
        {
            return NotFound("No Cv found for this id");
        }

        return Ok(student.cvLink);
    }

    
}
