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
}