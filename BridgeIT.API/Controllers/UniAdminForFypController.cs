using BridgeIT.API.DTOs.FypDTOs;
using BridgeIT.API.services.Implementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BridgeIT.Infrastructure;

namespace BridgeIT.API.Controllers;

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
        
        await _mailService.SendFypMail(student.User.Email!, fyp.Title!, "Approved");
        
        return Ok("FYP approved successfully.");
    }

    [HttpPut("reject-fyp")]
    public async Task<IActionResult> RejectFyp([FromQuery] Guid fypId)
    {
        var fyp = await _dbContext.Fyps
            .FirstOrDefaultAsync(f => f.Id == fypId);
        var student = await _dbContext.Students
            .Include(u => u.User)
            .FirstOrDefaultAsync(f => f.FypId == fypId);
        
        
        if (fyp == null || student == null)
        {
            return BadRequest("FYP not found.");
        }
        
        if (student.User == null)
        {
            return BadRequest("Something went wrong.");
        }
        
        fyp.Status = "Rejected";
        
        await _dbContext.SaveChangesAsync();
        
        await _mailService.SendFypMail(student.User.Email!, fyp.Title!, "Rejected");
        
        return Ok("FYP Rejected successfully.");
    }
    
    //TO-DO Get Fyps requests for uniAdmin for their university only DONE
    [HttpGet("get-fyps-for-uniAdmin-for-approval")]
    public async Task<IActionResult> GetFypsForUniAdminApproval([FromQuery] Guid uniId)
    {
        var students = await _dbContext.Students
            .Include(u => u.User)
            .Include(u => u.University)
            .Include(s => s.Fyp)
            .Where(s => (s.UniversityId == uniId && s.FypId != null) && s.Fyp!.Status == "Pending")
            .ToListAsync();

        if (!students.Any())
        {
            return BadRequest("No FYPs found.");
        }

        var dtoList = students.Select(f => new GetFypsRequestsForUniAdminDTO
        {
            FId = f.Fyp!.Id,
            Title = f.Fyp.Title!,
            FypId = f.Fyp.fyp_id,
            Members = f.Fyp.Members,
            Batch = f.Fyp.Batch!,
            Technology = f.Fyp.Technology!,
            Description = f.Fyp.Description!,
            Status = f.Fyp.Status!,
            StudentId = f.Id,
            StudentName = f.User!.FirstName + " " + f.User.LastName,
            StudentEmail = f.User.Email!,
            StudentRollNo = f.RollNumber,
            UniId = f.University!.Id,
            UniName = f.University.Name!,
        }).ToList();

        return Ok(dtoList);
    }
    
}