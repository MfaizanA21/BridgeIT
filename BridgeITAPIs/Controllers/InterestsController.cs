using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BridgeITAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterestsController : ControllerBase
    {
        private readonly BridgeItContext _dbContext;

        public InterestsController(BridgeItContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("post-interest")]
        public async Task<IActionResult> AddInterest([FromBody] string interest)
        {
            if (interest == null)
            {
                return BadRequest("Interest Data is null.");
            }

            var interests = new FieldOfInterest
            {
                Id = Guid.NewGuid(),
                FieldOfInterest1 = interest
            };
            
            await _dbContext.Set<FieldOfInterest>().AddAsync(interests);
            await _dbContext.SaveChangesAsync();
            return Ok("Interest saved Successfully!");
        }

        [HttpGet("get-interests")]
        public async Task<IActionResult> GetInterests()
        {
            var interests = await _dbContext.FieldOfInterests.ToListAsync();

            if (interests == null)
            {
                return NotFound("Interests not found.");
            }

            var dtoList = interests.Select(i => new FacultyInterestDTO
            {
                Id = i.Id,
                Interest = i.FieldOfInterest1 ?? string.Empty
            }).ToList();

            return Ok(dtoList);
        }

    }
}
