using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BridgeITAPIs.Controllers;

[Route("api/uni-admin-for-fyp")]
[ApiController]
public class UniAdminForFypController : Controller
{
    private readonly BridgeItContext _dbContext;
    private readonly MailService _mailService;
    
    public UniAdminForFypController(BridgeItContext dbContext, MailService mailService)
    {
        _dbContext = dbContext;
        _mailService = mailService;
    }

    [HttpPut("approve-fyp")]
    public async Task<IActionResult> ApproveFyp([FromQuery] Guid fypId)
    {
        var fyp = await _dbContext.Fyps
            .FirstOrDefaultAsync(f => f.Id == fypId);
        var student = await _dbContext.Students
            .Include(u => u.User)
            .FirstOrDefaultAsync(f => f.FypId == fypId);
        
        Console.WriteLine(student.User.Email);
        
        if (fyp == null || student == null)
        {
            return BadRequest("FYP not found.");
        }
        
        if (student.User == null)
        {
            return BadRequest("Something went wrong.");
        }
        
        fyp.Status = "Approved";
        
        await _dbContext.SaveChangesAsync();
        
        await _mailService.SendFypMail(student.User.Email, fyp.Title, "Approved");
        
        return Ok("FYP approved successfully.");
        // return Ok(fyp);
    }
    
}