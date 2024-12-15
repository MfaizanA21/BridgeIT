using Microsoft.AspNetCore.Mvc;
using BridgeITAPIs.Models;
using BridgeITAPIs.DTOs.FypDTOs;
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

    [HttpPost("register-fyp/")]
    public async Task<IActionResult> RegisterFyp([FromBody] RegisterFypDTO fypDTO, [FromQuery] Guid studentId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var student = await _dbContext.Students
            .Include(s => s.Fyps)
            .FirstOrDefaultAsync(s => s.Id == studentId);
        
        if (student == null)
        {
            return NotFound("Student not found.");
        }
        
        var fyp = new Fyp
        {
            Id = Guid.NewGuid(),
            fyp_id = fypDTO.fyp_id,
            Title = fypDTO.Title,
            Members = fypDTO.Members,
            Status = "Pending",
            Batch = fypDTO.Batch,
            Technology = fypDTO.Technology,
            Description = fypDTO.Description,
            // UniId = fypDTO.UniId
        };
        await _dbContext.Fyps.AddAsync(fyp);
        student.Fyps.Add(fyp);
        await _dbContext.SaveChangesAsync();

        // var studentFyp = new Dictionary<string, object>
        // {
        //     { "studentId", studentId },
        //     { "fypId", fyp.Id }
        // };
        //
        // await _dbContext.Set<Dictionary<string, object>>("StudentFyp").AddAsync(studentFyp);
        // await _dbContext.SaveChangesAsync();
        
        return Ok("FYP registered successfully and is awaiting approval.");
    }
    
}