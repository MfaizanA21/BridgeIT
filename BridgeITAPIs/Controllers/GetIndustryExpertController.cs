using Microsoft.AspNetCore.Mvc;
using BridgeITAPIs.DTOs.IndustryExpertDTOs;
using Microsoft.EntityFrameworkCore;
namespace BridgeITAPIs.Controllers;

[Route("api/get-industry-expert")]
[ApiController]
public class GetIndustryExpertController : ControllerBase
{
    private readonly BridgeItContext _dbContext;

    public GetIndustryExpertController(BridgeItContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("industry-expert-by-id/{userId}")]
    public async Task<IActionResult> GetIndustryExpert(Guid userId)
    {
        var industryExpert = await _dbContext.IndustryExperts
            .Include(f => f.User)
            .Include(f => f.Company)
            .FirstOrDefaultAsync(f => f.UserId == userId);

        if (industryExpert == null)
        {
            return NotFound("Industry Expert not found.");
        }

        var dtoList = new GetIndustryExpertDTO
        {
            UserId = industryExpert.UserId,
            IndExptId = industryExpert.Id,
            CompanyId = industryExpert.CompanyId,
            FirstName = industryExpert.User?.FirstName ?? string.Empty,
            LastName = industryExpert.User?.LastName ?? string.Empty,
            Email = industryExpert.User?.Email ?? string.Empty,
            ImageData = industryExpert.User?.ImageData ?? Array.Empty<byte>(),
            Description = industryExpert.User?.description ?? string.Empty,
            CompanyName = industryExpert.Company?.Name ?? string.Empty,
            Address = industryExpert.Company?.Address ?? string.Empty,
            //Interest = industryExpert.Interest != null ? new List<string> { industryExpert.Interest } : new List<string>()
            Contact = industryExpert.Contact ?? string.Empty,
        };

        return Ok(dtoList);
    }

    [HttpGet("industry-experts")]
    public async Task<IActionResult> GetIndustryExperts()
    {
        var industryExperts = await _dbContext.IndustryExperts
            .Include(f => f.User)
            .Include(f => f.Company)
            .ToListAsync();

        if (industryExperts == null)
        {
            return NotFound("Industry Expert not found.");
        }

        var dtoList = industryExperts.Select(f => new GetIndustryExpertDTO
        {
            IndExptId = f.Id,
            UserId = f.UserId,
            CompanyId = f.CompanyId,
            FirstName = f.User?.FirstName ?? string.Empty,
            LastName = f.User?.LastName ?? string.Empty,
            Email = f.User?.Email ?? string.Empty,
            ImageData = f.User?.ImageData ?? Array.Empty<byte>(),
            Description = f.User?.description ?? string.Empty,
            CompanyName = f.Company?.Name ?? string.Empty,
            Address = f.Company?.Address ?? string.Empty,
            //Interest = f.Interest != null ? new List<string> { f.Interest } : new List<string>()
            Contact = f.Contact ?? string.Empty,
        })
            .ToList();

        return Ok(dtoList);
    }

    [HttpGet("industry-expert-by-name/{name}")]
    public async Task<IActionResult> GetIndustryExpertByName(string name)
    {
        var industryExpert = await _dbContext.IndustryExperts
            .Include(f => f.User)
            .Include(f => f.Company)
            .Where(i => i.User != null &&
                    (i.User.FirstName != null && i.User.FirstName.ToLower().Contains(name.ToLower()) ||
                     i.User.LastName != null && i.User.LastName.ToLower().Contains(name.ToLower())))
            .ToListAsync();

        if (industryExpert == null || !industryExpert.Any())
        {
            return BadRequest("User not found.");
        }

        var dtoList = industryExpert.Select(f => new GetIndustryExpertDTO
        {
            IndExptId = f.Id,
            UserId = f.UserId,
            CompanyId = f.CompanyId,
            FirstName = f.User?.FirstName ?? string.Empty,
            LastName = f.User?.LastName ?? string.Empty,
            Email = f.User?.Email ?? string.Empty,
            ImageData = f.User?.ImageData ?? Array.Empty<byte>(),
            Description = f.User?.description ?? string.Empty,
            CompanyName = f.Company?.Name ?? string.Empty,
            Address = f.Company?.Address ?? string.Empty,
            //Interest = f.Interest != null ? new List<string> { f.Interest } : new List<string>()
            Contact = f.Contact ?? string.Empty,
        })
            .ToList();

        return Ok(dtoList);
    }
}
