using Microsoft.AspNetCore.Mvc;
using BridgeITAPIs.DTOs.UniAdminDTOs;
using Microsoft.EntityFrameworkCore;

namespace BridgeITAPIs.Controllers;

[Route("api/get-uni-admins")]
[ApiController]
public class GetUniAdminsController : ControllerBase
{
    private readonly BridgeItContext _dbcontext;

    public GetUniAdminsController(BridgeItContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    [HttpGet("all-admins")]
    public async Task<IActionResult> GetAllUniAdmins()
    {
        var admins = await _dbcontext.UniversityAdmins
            .Include(u => u.User)
            .Include(u => u.Uni)
            .ToListAsync();

        if (admins.Count == 0)
        {
            return NotFound("No Record Of Admins");
        }

        var adminsDTO = admins.Select(a => new GetUniAdminDTO
        {
            Id = a.Id,
            UserId = a.UserId,
            UniId = a.UniId,
            FirstName = a.User?.FirstName ?? string.Empty,
            LastName = a.User?.LastName ?? string.Empty,
            Email = a.User?.Email ?? string.Empty,
            ProfileImage = a.User?.ImageData ?? Array.Empty<byte>(),
            Description = a.User?.description ?? string.Empty,
            University = a.Uni?.Name ?? string.Empty,
            OfficeAddress = a.OfficeAddress ?? string.Empty,
            Contact = a.Contact ?? string.Empty
        }).ToList();

        return Ok(adminsDTO);
    }

    [HttpGet("admins-by-id/{id}")]
    public async Task<IActionResult> GetUniAdminById(Guid id)
    {
        var admin = await _dbcontext.UniversityAdmins
            .Include(u => u.User)
            .Include(u => u.Uni)
            .FirstOrDefaultAsync(u => u.UserId == id);

        if (admin == null)
        {
            return NotFound("No Record Found");
        }

        var adminDTO = new GetUniAdminDTO
        {
            Id = admin.Id,
            UserId = admin.UserId,
            UniId = admin.UniId,
            FirstName = admin.User?.FirstName ?? string.Empty,
            LastName = admin.User?.LastName ?? string.Empty,
            Email = admin.User?.Email ?? string.Empty,
            ProfileImage = admin.User?.ImageData ?? Array.Empty<byte>(),
            Description = admin.User?.description ?? string.Empty,
            University = admin.Uni?.Name ?? string.Empty,
            OfficeAddress = admin.OfficeAddress ?? string.Empty,
            Contact = admin.Contact ?? string.Empty
        };

        return Ok(adminDTO);
    }

    [HttpGet("admins-by-university/{name}")]
    public async Task<IActionResult> GetUniAdminByUniversity(string name)
    {
        var admin = await _dbcontext.UniversityAdmins
            .Include(u => u.User)
            .Include(u => u.Uni)
            .Where(u => u.Uni.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();

        if (admin.Count == 0)
        {
            return NotFound("No Record Found");
        }

        var adminDTO = admin.Select(a => new GetUniAdminDTO
        {
            Id = a.Id,
            UserId = a.UserId,
            UniId = a.UniId,
            FirstName = a.User?.FirstName ?? string.Empty,
            LastName = a.User?.LastName ?? string.Empty,
            Email = a.User?.Email ?? string.Empty,
            ProfileImage = a.User?.ImageData ?? Array.Empty<byte>(),
            Description = a.User?.description ?? string.Empty,
            University = a.Uni?.Name ?? string.Empty,
            OfficeAddress = a.OfficeAddress ?? string.Empty,
            Contact = a.Contact ?? string.Empty
        }).ToList();

        return Ok(adminDTO);
    }

    [HttpGet("admins-by-university-id/{UniId}")]
    public async Task<IActionResult> GetUniAdminByUniversityId(Guid UniId)
    {
        var admin = await _dbcontext.UniversityAdmins
            .Include(u => u.User)
            .Include(u => u.Uni)
            .Where(u => u.UniId == UniId)
            .ToListAsync();

        if (admin.Count == 0)
        {
            return NotFound("No Record Found");
        }

        var adminDTO = admin.Select(a => new GetUniAdminDTO
        {
            Id = a.Id,
            UserId = a.UserId,
            UniId = a.UniId,
            FirstName = a.User?.FirstName ?? string.Empty,
            LastName = a.User?.LastName ?? string.Empty,
            Email = a.User?.Email ?? string.Empty,
            ProfileImage = a.User?.ImageData ?? Array.Empty<byte>(),
            Description = a.User?.description ?? string.Empty,
            University = a.Uni?.Name ?? string.Empty,
            OfficeAddress = a.OfficeAddress ?? string.Empty,
            Contact = a.Contact ?? string.Empty
        }).ToList();

        return Ok(adminDTO);
    }

    [HttpGet("admins-by-admin-id/{id}")]
    public async Task<IActionResult> GetUniAdminByAdminId(Guid id)
    {
        var admin = await _dbcontext.UniversityAdmins
            .Include(u => u.User)
            .Include(u => u.Uni)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (admin == null)
        {
            return NotFound("No Record Found");
        }

        var adminDTO = new GetUniAdminDTO
        {
            Id = admin.Id,
            UserId = admin.UserId,
            UniId = admin.UniId,
            FirstName = admin.User?.FirstName ?? string.Empty,
            LastName = admin.User?.LastName ?? string.Empty,
            Email = admin.User?.Email ?? string.Empty,
            ProfileImage = admin.User?.ImageData ?? Array.Empty<byte>(),
            Description = admin.User?.description ?? string.Empty,
            University = admin.Uni?.Name ?? string.Empty,
            OfficeAddress = admin.OfficeAddress ?? string.Empty,
            Contact = admin.Contact ?? string.Empty
        };

        return Ok(adminDTO);
    }

    
}
