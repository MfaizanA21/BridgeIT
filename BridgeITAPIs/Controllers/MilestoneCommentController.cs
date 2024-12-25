using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BridgeITAPIs.Controllers;

[Route("api/milestone-comment")]
[ApiController]
public class MilestoneCommentController : Controller
{
    private readonly BridgeItContext _dbContext;
    
    public MilestoneCommentController(BridgeItContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("add-milestone-comment")]
    public async Task<IActionResult> AddMilestoneComment([FromQuery] Guid milestoneId, [FromQuery] Guid expertId,
        [FromBody] string comment)
    {

        var stone = _dbContext.MileStones
            .Include(m => m.Project)
            .ThenInclude(p => p.IndExpert)
            .Where(p => p.Id == milestoneId && p.Project.IndExpert.Id == expertId);

        if (!stone.Any())
        {
            return BadRequest("Authorization failed: either milestone does not exist or expert is not authorized.");
        }

        var milestone_comment = new MilestoneComment
        {
            Id = Guid.NewGuid(),
            Comment = comment,
            CommentDate = DateTime.UtcNow,
            Milestone_id = milestoneId,
            Commenter_id = expertId,
        };

        await _dbContext.MilestoneComments.AddAsync(milestone_comment);
        await _dbContext.SaveChangesAsync();

        return Ok("Comment Added Successfully.");
    }
}