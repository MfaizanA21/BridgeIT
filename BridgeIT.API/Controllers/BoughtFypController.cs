using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BridgeIT.Infrastructure;

namespace BridgeIT.API.Controllers;

[ApiController]
[Route("api/bought-fyp")]
public class BoughtFypController : ControllerBase
{
    private readonly BridgeItContext _dbContext;
    
    public BoughtFypController(BridgeItContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllBoughtFyps()
    {
        var boughtFyps = await _dbContext.BoughtFyps
            .Include(b => b.Fyp)
            .Include(b => b.IndExpert)
            .ToListAsync();

        if (!boughtFyps.Any())
        {
            return NotFound("No bought Fyps found.");
        }
        
        var bfypl = new List<string>();

        foreach (var boughtFyp in boughtFyps)
        {
            bfypl.Add(boughtFyp.Id.ToString());
        }

        return Ok(bfypl);
    }

    [HttpGet("by-id/{id}")]
    public async Task<IActionResult> BoughtFypById(Guid id)
    {
        var boughtFyp = await _dbContext.BoughtFyps
            .Include(b => b.Fyp)
            .Include(b => b.IndExpert)
            .ThenInclude(i => i.User)
            .FirstOrDefaultAsync(b => b.Id == id || b.FypId == id || b.IndExpertId == id);

        if (boughtFyp == null)
        {
            return NotFound("Bought FYP not found.");
        }
        
        var dto = new
        {
            boughtFyp.Id,
            boughtFyp.FypId,
            boughtFyp.IndExpertId,
            boughtFyp.Price,
            FypTitle = boughtFyp.Fyp.Title,
            IndExpertName = $"{boughtFyp.IndExpert.User.FirstName} {boughtFyp.IndExpert.User.LastName}",
        };
        
        return Ok(dto);
    }
    
    [HttpPatch("add-agreement/{id}")]
    public async Task<IActionResult> AddAgreement(Guid id, [FromBody] string agreementDoc)
    {
        var boughtFyp = await _dbContext.BoughtFyps
            .FirstOrDefaultAsync(b => b.Id == id);

        if (boughtFyp == null)
        {
            return NotFound("Bought FYP not found.");
        }
        
        byte[] agreementBytes;
        try
        {
            agreementBytes = Convert.FromBase64String(agreementDoc);
        }
        catch
        {
            return BadRequest("Invalid base64 PDF data.");
        }

        boughtFyp.Agreement = agreementBytes;
        
        await _dbContext.SaveChangesAsync();
        
        return Ok("Agreement added successfully.");
    }
}