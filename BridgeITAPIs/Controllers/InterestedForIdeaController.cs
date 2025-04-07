using BridgeITAPIs.DTOs.InterestedForIdeaDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BridgeITAPIs.Controllers;

[Route("api/interested-for-idea")]
[ApiController]
public class InterestedForIdeaController : Controller
{
    private readonly BridgeItContext _dbContext;
    private readonly MailService _mailService;

    public InterestedForIdeaController(BridgeItContext dbContext, MailService mailService)
    {
        _dbContext = dbContext;
        _mailService = mailService;
    }

    [HttpPost("student-interested-for-idea/{stdId}/{ideaId}")]
    public async Task<IActionResult> StudentInterestedForIdea(Guid stdId, Guid ideaId)
    {
        var student = await _dbContext.Students
            .Include(u => u.User)
            .FirstOrDefaultAsync(s => s.Id == stdId || s.UserId == stdId);
        
        var idea = await _dbContext.Ideas
            .Include(i => i.Faculty)
            .ThenInclude(u => u.User)
            .FirstOrDefaultAsync(i => i.Id == ideaId);
        
        if (student == null || idea == null)
        {
            return NotFound("Student/Idea not found.");
        }
        
        var interested = new InterestedForIdea
        {
            Id = Guid.NewGuid(),
            StudentId = stdId,
            IdeaId = ideaId,
            Status = null
        };
        
        await _dbContext.InterestedForIdeas.AddAsync(interested);
        await _dbContext.SaveChangesAsync();
        
        await _mailService.SendStudentInterestedForIdeaMailToFaculty(idea.Faculty.User.Email, idea.Name, student.User.FirstName + " " + student.User.LastName);
        
        return Ok("Request Placed Successfully.");
    }

    [HttpGet("get-interested-students-requests/{facultyId}")]
    public async Task<IActionResult> GetInterestedStudents(Guid facultyId)
    {
        var requests = await _dbContext.InterestedForIdeas
            .Include(r => r.Student)
            .ThenInclude(r => r!.User)
            .Include(r => r.Idea)
            .ThenInclude(r => r!.Faculty)
            .Where(r => r.Idea!.FacultyId == facultyId || r.Status != 1)
            .ToListAsync();

        if (!requests.Any())
        {
            return NotFound("No requests found.");
        }

        var groupedData = requests
            .GroupBy(r => r.IdeaId)
            .Select(g => new
            {
                IdeaId = g.Key,
                IdeaName = g.First().Idea!.Name,
                Requests = g.Select(request => new GetRequestsDTO
                {
                    Id = request.Id,
                    IdeaId = request.IdeaId,
                    StudentId = request.StudentId,
                    FacultyId = request.Idea?.FacultyId,
                    StdUserId = request.Student?.UserId,
                    FacUserId = request.Idea?.Faculty?.UserId,
                    IdeaName = request.Idea.Name,
                    StudentName = $"{request.Student?.User?.FirstName} {request.Student?.User?.LastName}",
                    StudentDept = request.Student.department,
                }).ToList()
            }).ToList();

        return Ok(groupedData);
    }

    [HttpGet("get-request-details-by-id/{RequestId}")]
    public async Task<IActionResult> GetRequestDetailsById(Guid RequestId)
    {
        var requestDetails = await _dbContext.InterestedForIdeas
            .Include(r => r.Student)
            .ThenInclude(r => r!.User)
            .Include(r => r.Idea)
            .ThenInclude(r => r!.Faculty)
            .ThenInclude(u => u!.User)
            .FirstOrDefaultAsync(r => r.Id == RequestId);

        if (requestDetails == null)
        {
            return BadRequest("No request Found.");
        }

        var detail = new GetRequestDetailDTO
        {
            Id = requestDetails.Id,
            IdeaId = requestDetails.IdeaId,
            StudentId = requestDetails.StudentId,
            FacultyId = requestDetails.Idea?.FacultyId,
            StdUserId = requestDetails.Student?.UserId,
            FacUserId = requestDetails.Idea?.Faculty?.UserId,
            IdeaName = requestDetails.Idea?.Name ?? string.Empty,
            StudentName = $"{requestDetails.Student?.User?.FirstName} {requestDetails.Student?.User?.LastName}",
            StudentDept = requestDetails.Student?.department ?? String.Empty,
            StudentEmail = requestDetails.Student?.User?.Email ?? String.Empty,
            StudentRollNumber = requestDetails.Student?.RollNumber ?? null
        };

        return Ok(detail);
    }

    [HttpPut("accept-request/{RequestId}")]
    public async Task<IActionResult> AcceptRequest(Guid RequestId, [FromBody] string time)
    {
        var request = await _dbContext.InterestedForIdeas
            .Include(r => r.Student)
            .ThenInclude(r => r!.User)
            .Include(r => r.Idea)
            .ThenInclude(r => r!.Faculty)
            .ThenInclude(u => u!.User)
            .FirstOrDefaultAsync(r => r.Id == RequestId);

        if (request == null)
        {
            return NotFound("Request Not Found");
        }
        
        request.Status = 1;
        
        await _dbContext.SaveChangesAsync();

        await _mailService.SendInterestAcceptanceMailToStudent(request.Student.User.Email, request.Idea.Name,request.Idea.Faculty.User.Email, time);

        return Ok("Request have been accepted");
    }
    
    [HttpPut("reject-request/{RequestId}")]
    public async Task<IActionResult> RejectRequest(Guid RequestId)
    {
        var request = await _dbContext.InterestedForIdeas
            .Include(r => r.Student)
            .ThenInclude(r => r!.User)
            .Include(r => r.Idea)
            .ThenInclude(r => r!.Faculty)
            .ThenInclude(u => u!.User)
            .FirstOrDefaultAsync(r => r.Id == RequestId);

        if (request == null)
        {
            return NotFound("Request Not Found");
        }
        
        request.Status = 0;
        
        await _dbContext.SaveChangesAsync();

        await _mailService.SendInterestRejectionMailToStudent(request.Student.User.Email, request.Idea.Name,request.Idea.Faculty.User.Email);

        return Ok("Request have been accepted");
    }
}