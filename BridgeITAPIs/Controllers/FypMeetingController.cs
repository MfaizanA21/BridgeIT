using BridgeITAPIs.DTOs.FypMeetingDtos;
using BridgeITAPIs.services.Implementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BridgeITAPIs.Controllers;


[Route("api/fyp-meeting")]
[ApiController]
public class FypMeetingController : ControllerBase
{
    private readonly BridgeItContext _dbContext;
    private readonly MailService _mailService;
    
    public FypMeetingController(BridgeItContext dbContext, MailService mailService)
    {
        _dbContext = dbContext;
        _mailService = mailService;
    }
    
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllFypMeetings()
    {
        var fypMeetings = await _dbContext.FypMeetings
            .Include(f => f.Fyp)
            .Include(f => f.IndustryExpert)
            .ToListAsync();

        if (!fypMeetings.Any())
        {
            return NotFound("No FYP meetings found.");
        }

        return Ok(fypMeetings[0].Id);
    }

    [HttpPost("add/{fypId}")]
    public async Task<IActionResult> AddFypMeeting(Guid fypId, [FromBody] AddmeetingInitDto addmeetingInitDto)
    {
        var fyp = await _dbContext.Fyps
            .Include(f => f.Students)
            .ThenInclude(s => s.User)
            .FirstOrDefaultAsync(f => f.Id == fypId);

        if (fyp == null)
        {
            return NotFound("FYP not found.");
        }

        var newFypMeeting = new FypMeeting
        {
            Id = Guid.NewGuid(),
            FypId = fyp.Id,
            IndExpId = addmeetingInitDto.IndExpertId,
            ChosenSlot = addmeetingInitDto.TimeSlot,
            IsMeetDone = false
        };

        var expert = await _dbContext.IndustryExperts.Include(i => i.User)
            .FirstOrDefaultAsync(i => i.Id == addmeetingInitDto.IndExpertId);
        var expertName = expert.User.FirstName + " " + expert.User.LastName;

        foreach (var student in fyp.Students)
        {
            await _mailService.NotifyStudentAboutMeetingTime(student.User.Email, expertName, fyp.Title, addmeetingInitDto.TimeSlot);
        }

        await _dbContext.FypMeetings.AddAsync(newFypMeeting);
        await _dbContext.SaveChangesAsync();

        return Ok("FYP meeting added successfully.");
    }
    
    [HttpPatch("add-link/{id}")]
    public async Task<IActionResult> AddMeetingLink(Guid id, [FromBody] string meetLink)
    {
        var fypMeeting = await _dbContext.FypMeetings
            .FirstOrDefaultAsync(f => f.Id == id || f.FypId == id);

        if (fypMeeting == null)
        {
            return NotFound("FYP meeting not found.");
        }
        
        fypMeeting.MeetLink = meetLink;
        await _dbContext.SaveChangesAsync();

        return Ok("Meeting link added successfully.");
    }
    
    [HttpGet("by-id/{id}")]
    public async Task<IActionResult> GetFypMeetingsByExpert(Guid id)
    {
        var fypMeetings = await _dbContext.FypMeetings
            .Include(f => f.Fyp)
            .Include(f => f.IndustryExpert)
            .Where(f => f.IndExpId == id || f.Id == id || f.FypId == id)
            .ToListAsync();

        if (!fypMeetings.Any())
        {
            return NotFound("No FYP meetings found for this expert.");
        }

        var meetingDtos = fypMeetings.Select(f => new GetMeetingDto
        {
            id = f.Id,
            status = f.Status,
            meetLink = f.MeetLink,
            feedback = f.Feedback,
            isMeetDone = f.IsMeetDone,
            chosenSlot = f.ChosenSlot,
            fypId = f.FypId,
            fypTitle = f.Fyp.Title,
            indExpId = f.IndExpId
        }).ToList();

        return Ok(meetingDtos);
    }

    [HttpPatch("after-meeting/{id}")]
    public async Task<IActionResult> AfterMeetingFeedback(Guid id, [FromBody] AfterMeetingFeedbackDto meetingdto)
    {
        var meeting = await _dbContext.FypMeetings.FirstOrDefaultAsync(f => f.Id == id || f.FypId == id);

        if (meeting == null)
        {
            return NotFound("No meeting found against the id");
        }

        meeting.Feedback = meetingdto.Feedback;
        meeting.Status = meetingdto.status;
        meeting.IsMeetDone = true;
        
        await _dbContext.SaveChangesAsync();

        return Ok("Entity updated successfully");
    }

    [HttpPost("sponsor-fyp/{id}")]
    public async Task<IActionResult> SponsoreFyp(Guid id, [FromForm] SponsorFypDto dto)
    {
        var fyp = await _dbContext.FypMeetings
            .Include(f => f.Fyp)
            .ThenInclude(s => s.Students)
            .FirstOrDefaultAsync(f => f.Id == id || f.Fyp.Id == id);
    
        if (fyp == null)
        {
            return NotFound("FYP not found.");
        }
        
        var expert = await _dbContext.IndustryExperts.Include(i => i.User)
            .FirstOrDefaultAsync(i => i.Id == dto.ExpertId);
    
        if (expert == null)
        {
            return NotFound("Expert not found");
        }
        
        byte[] agreementBytes;
        try
        {
            agreementBytes = Convert.FromBase64String(dto.AgreementBase64);
        }
        catch
        {
            return BadRequest("Invalid base64 PDF data.");
        }
        
        var SponsorFyp = new SponsoredFyp()
        {
            Id = Guid.NewGuid(),
            FypId = fyp.Fyp.Id,
            SponsoredById = dto.ExpertId,
            Agreement = agreementBytes
        };
        
        await _dbContext.SponsoredFyps.AddAsync(SponsorFyp);
        await _dbContext.SaveChangesAsync();
        
        return Ok("FYP sponsored successfully.");
    }
    
    [HttpGet("get-sponsored-fyp/{id}")]
    public async Task<IActionResult> GetSponsoredFyp(Guid id)
    {
        var sponsoredFyp = await _dbContext.SponsoredFyps
            .Include(f => f.Fyp)
            .Include(f => f.SponsoredBy)
            .ThenInclude(i => i.User)
            .FirstOrDefaultAsync(f => f.Id == id || f.FypId == id);

        if (sponsoredFyp == null)
        {
            return NotFound("No sponsored FYP found.");
        }
        
        var dto = new GetSponsoredFypDto
        {
            Id = sponsoredFyp.Id,
            FypId = sponsoredFyp.FypId,
            SponsoredById = sponsoredFyp.SponsoredById,
            AgreementBase64 = Convert.ToBase64String(sponsoredFyp.Agreement),
            SponsoredByName = $"{sponsoredFyp.SponsoredBy.User.FirstName} {sponsoredFyp.SponsoredBy.User.LastName}"
        };

        return Ok(dto);
    }

    [HttpGet("is-sponsored/{id}")]
    public async Task<Boolean> IsItSponsored(Guid id)
    {
        var sponsoredFyp = await _dbContext.SponsoredFyps
            .Include(f => f.Fyp)
            .FirstOrDefaultAsync(f => f.FypId == id);

        if (sponsoredFyp == null)
        {
            return false;
        }

        return true;
    }
}