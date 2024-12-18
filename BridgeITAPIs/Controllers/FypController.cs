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
            .FirstOrDefaultAsync(s => s.Id == studentId);
        
        if (student == null)
        {
            return NotFound("Student not founds.");
        }
        
        if (student.FypId != null)
        {
            return BadRequest("Student has already registered a FYP.");
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
        };
        await _dbContext.Fyps.AddAsync(fyp);
        student.FypId = fyp.Id;
        await _dbContext.SaveChangesAsync();
        
        return Ok("FYP registered successfully and is awaiting approval.");
    }
    
    
}