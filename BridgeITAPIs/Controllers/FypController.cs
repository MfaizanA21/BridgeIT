using Microsoft.AspNetCore.Mvc;
using BridgeITAPIs.DTOs.FypDTOs;
using BridgeITAPIs.DTOs.FypDTOs.DetailedFypDTOs;
using Microsoft.EntityFrameworkCore;

namespace BridgeITAPIs.Controllers;

[Route("api/fyp")]
[ApiController]
public class FypController : Controller
{
    private readonly BridgeItContext _dbContext;
    
    public FypController(BridgeItContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("register-fyp")]
    public async Task<IActionResult> RegisterFyp([FromBody] RegisterFypDTO fypDTO, [FromQuery] Guid studentId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        string Status;
        Student? student = null;
        
        if (studentId != Guid.Empty)
        {
            student = await _dbContext.Students
                .Include(s => s.Fyp)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null)
            {
                return NotFound("Student not found.");
            }

            if (student.FypId != null && student.Fyp!.Status != "Rejected")
            {
                return BadRequest("Student has already registered a FYP or requested it.");
            }
            Status = "Pending";
        }
        else
        {
            Status = "Approved";
        }
        
        var fyp = new Fyp
        {
            Id = Guid.NewGuid(),
            fyp_id = fypDTO.fyp_id,
            Title = fypDTO.Title,
            Members = fypDTO.Members,
            Status = Status,
            Batch = fypDTO.Batch,
            Technology = fypDTO.Technology,
            Description = fypDTO.Description,
            FacultyId = fypDTO.FacultyId
        };
        await _dbContext.Fyps.AddAsync(fyp);
        if (student != null)
        {
            student.FypId = fyp.Id;
        }
        await _dbContext.SaveChangesAsync();
        
        return Ok("FYP registered successfully and is awaiting approval.");
    }

    [HttpGet("get-fyp-by-id/{fypId}")]
    public async Task<IActionResult> GetFypById(Guid fypId)
    {
        var fyp = await _dbContext.Fyps
            .FirstOrDefaultAsync(f => f.Id == fypId);

        if (fyp == null)
        {
            return BadRequest("FYP not found.");
        }

        var dto = new GetFypByIdDTO
        {
            Id = fyp.Id,
            FypId = fyp.fyp_id,
            Title = fyp.Title ?? string.Empty,
            Members = fyp.Members,
            Status = fyp.Status ?? string.Empty,
            Batch = fyp.Batch ?? string.Empty,
            Technology = fyp.Technology ?? string.Empty,
            Description = fyp.Description ?? string.Empty
        };
        
        return Ok(dto);
    }

    [HttpPost("request-to-add-fyp/{fypId}")]
    public async Task<IActionResult> RequestToAddFyp(Guid fypId)
    {
        var fyp = await _dbContext.Students
            .Include(f => f.Fyp)
            .FirstOrDefaultAsync(f => f.FypId == fypId);
        
        if (fyp == null)
        {
            return NotFound("FYP not found.");
        }

        var request = new RequestForFyp
        {
            Id = Guid.NewGuid(),
            StudentId = fyp.Id,
            FypId = fypId,
            Status = null
        };

        await _dbContext.RequestForFyps.AddAsync(request);
        await _dbContext.SaveChangesAsync();
        
        return Ok("Request Sent!");
    }

    [HttpGet("get-fyp-by-faculty-id/{facultyId}")]
    public async Task<IActionResult> GetFypByFacultyId(Guid facultyId)
    {
        var fyp = await _dbContext.Fyps
            .Include(f => f.Faculty)
            .ThenInclude(u => u!.User)
            .Where(f => f.FacultyId != null && f.FacultyId == facultyId && f.Status != "Rejected")
            .ToListAsync();

        if (!fyp.Any())
        {
            return NotFound("Not Fyp found for this faculty");
        }

        var fypDto = fyp.Select(f => new GetFypForFacultyDTO
        {
            Id = f.Id,
            Title = f.Title ?? string.Empty,
            FypId = f.fyp_id,
            Description = f.Description ?? string.Empty,
            Members = f.Members
        }).ToList();

        return Ok(fypDto);

    }

    [HttpGet("get-detailed-fyp-by-id/{fypId}")]
    public async Task<IActionResult> GetDetailedFypById(Guid fypId)
    {
        var fyp = await _dbContext.Fyps
            .Include(f => f.Faculty)
            .ThenInclude(fac => fac!.User)
            .Include(f => f.Students)
            .ThenInclude(stu => stu.User)
            .Where(f => f.Id == fypId)
            .FirstOrDefaultAsync();

        if (fyp == null)
        {
            return BadRequest("No FYP exists against this Id.");
        }

        var detailedFypDTO = new DetailedFypDTO
        {
            Id = fyp.Id,
            Title = fyp.Title,
            Members = fyp.Members,
            Batch = fyp.Batch,
            Technology = fyp.Technology,
            Description = fyp.Description,
            Status = fyp.Status,
            Faculty = fyp.Faculty != null ? new FacultyDTO
            {
                Id = fyp.Faculty.Id,//
                Name = $"{fyp.Faculty.User?.FirstName} {fyp.Faculty.User?.LastName}",
                Interest = fyp.Faculty.Interest,
                Department = fyp.Faculty.Department,
                Post = fyp.Faculty.Post
            } : null,
            Students = fyp.Students.Select(student => new StudentDTO
            {
                Id = student.Id,
                Name = $"{student.User?.FirstName} {student.User?.LastName}",
                Department = student.department,
                RollNumber = student.RollNumber,
                CvLink = student.cvLink,
                Skills = student.skills
            }).ToList()
        };

        return Ok(detailedFypDTO);
    }

}