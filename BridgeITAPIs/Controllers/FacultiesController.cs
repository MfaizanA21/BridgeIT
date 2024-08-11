using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BridgeITAPIs.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FacultiesController : ControllerBase
{
    private readonly BridgeItContext _dbContext;

    public FacultiesController(BridgeItContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpDelete("delete-faculty/{userId}")]
    public async Task<IActionResult> DeleteFaculty(Guid userId)
    {
        var faculty = await _dbContext.Faculties
            .Include(f => f.User)
            .Include(f => f.Uni)
            .FirstOrDefaultAsync(f => f.UserId == userId);

        if (faculty == null)
        {
            return NotFound("Faculty not found.");
        }

        if (faculty.User != null)
        {
            _dbContext.Users.Remove(faculty.User);
        }

        _dbContext.Faculties.Remove(faculty);
        await _dbContext.SaveChangesAsync();

        return Ok("Faculty deleted successfully.");
    }
}
