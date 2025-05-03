using BridgeITAPIs.DTOs.IndExpertRequestFypDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BridgeITAPIs.Controllers;

[Route("api/ind-expert-request-fyp")]
[ApiController]
public class IndExpertRequestFypController : ControllerBase
{
    private readonly BridgeItContext _dbContext;
    private readonly MailService _mailService;
    
    public IndExpertRequestFypController(BridgeItContext dbContext, MailService mailService)
    {
        _dbContext = dbContext;
        _mailService = mailService;
    }
    
    [HttpGet("get-fyp-requests")]
    public async Task<IActionResult> GetFypRequests()
    {
        var fypRequests = await _dbContext.RequestForFyps
            .Include(f => f.IndustryExpert)
            .Include(f => f.Fyp)
            .ToListAsync();

        if (!fypRequests.Any())
        {
            return NotFound("No FYP requests found.");
        }

        return Ok(fypRequests[0].Id);
    }
    
    [HttpPost("add/{FypId}")]
    public async Task<IActionResult> AddFypRequest(Guid FypId, [FromBody] Guid IndustryExpertId)
    {
        var fyp = await _dbContext.Fyps
            .FirstOrDefaultAsync(f => f.Id == FypId);

        if (fyp == null)
        {
            return NotFound("FYP not found.");
        }

        var industryExpert = await _dbContext.IndustryExperts
            .FirstOrDefaultAsync(i => i.Id == IndustryExpertId);

        if (industryExpert == null)
        {
            return NotFound("Industry Expert not found.");
        }

        var newFypRequest = new RequestForFyp
        {
            Id = Guid.NewGuid(),
            FypId = fyp.Id,
            IndustryExpertId = industryExpert.Id,
            Status = null
        };
        
        //null -> Pending
        //0 -> Rejected
        //1 -> Accepted

        await _dbContext.RequestForFyps.AddAsync(newFypRequest);
        await _dbContext.SaveChangesAsync();

        return Ok("FYP request added successfully.");
    }
    
    [HttpPut("approve/{id}")]
    public async Task<IActionResult> ApproveFypRequest(Guid id)
    {
        var fypRequest = await _dbContext.RequestForFyps
            .Include(r => r.IndustryExpert)
            .ThenInclude(i => i.User)
            .Include(r => r.Fyp)
            .ThenInclude(f => f.Students)
            .ThenInclude(s => s.User)
            .FirstOrDefaultAsync(f => f.Id == id);

        if (fypRequest == null)
        {
            return NotFound("FYP request not found.");
        }

        fypRequest.Status = 1; // Approved
        await _dbContext.SaveChangesAsync();
        
        await _mailService.SendFypApprovalStatusMail(
            fypRequest.IndustryExpert!.User!.Email!,
            fypRequest.IndustryExpert!.User!.FirstName!,
            fypRequest.Fyp!.Title!,
            true
        );

        foreach (var student in fypRequest.Fyp.Students)
        {
            await _mailService.NotifyStudentFypInterest(student.User!.Email!, student.User!.FirstName!,
                fypRequest.IndustryExpert!.User!.FirstName!, fypRequest.IndustryExpert!.User!.Email!,
                fypRequest.Fyp!.Title!);
        }

        return Ok("FYP request approved successfully.");
    }
    
    [HttpPut("reject/{id}")]
    public async Task<IActionResult> RejectFypRequest(Guid id)
    {
        var fypRequest = await _dbContext.RequestForFyps
            .Include(r => r.IndustryExpert)
            .ThenInclude(i => i.User)
            .Include(r => r.Fyp)
            .FirstOrDefaultAsync(f => f.Id == id);
        
        
        if (fypRequest == null)
        {
            return NotFound("FYP request not found.");
        }

        fypRequest.Status = 0; // Rejected
        await _dbContext.SaveChangesAsync();
        
        await _mailService.SendFypApprovalStatusMail(
            fypRequest.IndustryExpert!.User!.Email!,
            fypRequest.IndustryExpert!.User!.FirstName!,
            fypRequest.Fyp!.Title!,
            false
        );
        
        return Ok("FYP request rejected successfully.");
    }
    
    [HttpGet("pending-for-admin/{uniId}")]
    public async Task<IActionResult> GetPendingFypRequestsForAdmin(Guid uniId)
    {
        var fypRequests = await _dbContext.RequestForFyps
            .Include(f => f.IndustryExpert)
            .ThenInclude(i => i.User)
            .Include(f => f.Fyp)
            .ThenInclude(f => f.Students)
            .Where(f => f.Status == null && 
                        f.Fyp!.Students.Any(s => s.UniversityId == uniId))
            .ToListAsync();

        if (!fypRequests.Any())
        {
            return NotFound("No pending FYP requests found.");
        }

        var dtoList = fypRequests.Select(r => new GetFypRequestDTO
        {
            Id = r.Id,
            Status = r.Status,
            StudentIds = r.Fyp!.Students.Select(s => s.Id).ToArray(),
            FypId = r.FypId,
            IndustryExpertId = r.IndustryExpertId,
            IndustryExpertName = r.IndustryExpert?.User != null
                ? $"{r.IndustryExpert.User.FirstName} {r.IndustryExpert.User.LastName}"
                : "N/A",
            FypTitle = r.Fyp.Title ?? string.Empty,
            FypDescription = r.Fyp.Description ?? string.Empty,
            Fyp_fypId = r.Fyp.fyp_id
        }).ToList();

        return Ok(dtoList);
    }
}