using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BridgeITAPIs.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BridgeITAPIs.Controllers
{
    [Route("api/[controller]")]
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
                .FirstOrDefaultAsync(s => s.Id == userId);

            if (student == null)
            {
                return NotFound("Student not found.");
            }

            var dto = new GetStudentDTO
            {
                FirstName = student.User?.FirstName ?? string.Empty,
                LastName = student.User?.LastName ?? string.Empty,
                Email = student.User?.Email ?? string.Empty,
                //Skills = student.Skills.Select(s => s.Skill1).ToList(),
                ImageData = student.User?.ImageData ?? string.Empty,
                UniversityName = student.University?.Name ?? string.Empty,
                Address = student.University?.Address ?? string.Empty,
                RollNumber = student?.RollNumber.ToString() ?? string.Empty
            };

            return Ok(dto);
        }

    }
}
