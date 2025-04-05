using System.Security.Claims;
using BridgeITAPIs.DTOs.ReviewDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BridgeITAPIs.Controllers;

[Route("api/reviews")]
[ApiController]
public class ReviewController : Controller
{
    private readonly BridgeItContext _context;
    
    public ReviewController(BridgeItContext context)
    {
        _context = context;
    }
    
    [HttpPost("add-review/{projectId}")]
    public async Task<IActionResult> AddReview(Guid projectId, [FromBody] AddReviewDto reviewDto)
    {
        if(reviewDto.Rating > 5 || reviewDto.Rating < 1)
        {
            return BadRequest("Rating must be between 1 and 5.");
        }
        
        var reviewer = await _context.Users.FindAsync(reviewDto.ReviewerId);
        
        if (reviewer == null || (reviewer.Role != "IndustryExpert" && reviewer.Role != "Faculty"))
        {
            return BadRequest("Reviewer must be a valid user with the IndustryExpert or Faculty role.");
        }
        
        var project = await _context.Projects.FindAsync(projectId);
        
        if (project == null)
        {
            return NotFound("Project not found.");
        }
        
        var review = new Review
        {
            Id = Guid.NewGuid(),
            ProjectId = projectId,
            ReviewerId = reviewDto.ReviewerId,
            Review1 = reviewDto.Review,
            Rating = reviewDto.Rating,
            DatePosted = DateTime.Today,
        };
        await _context.Reviews.AddAsync(review);
        await _context.SaveChangesAsync();

        return StatusCode(201, "review added successfully");

    }

    [HttpGet("get-reviews/{projectId}")]
    public async Task<IActionResult> GetReviewsById(Guid projectId)
    {
        var reviews = await _context.Reviews
            .Where(r => r.ProjectId == projectId)
            .Select(r => new
            {
                r.Id,
                r.Review1,
                r.Rating,
                r.DatePosted,
                ReviewerName = r.Reviewer != null ? $"{r.Reviewer.FirstName} {r.Reviewer.LastName}" : "Unknown"
            })
            .ToListAsync();

        if (reviews.Count == 0)
        {
            return NotFound("No reviews found for this project.");
        }
        
        return Ok(reviews);
    }
    
    [Authorize(Roles = "IndustryExpert, Faculty")]
    [HttpDelete("delete-review/{reviewId}")]
    public async Task<IActionResult> DeleteReview(Guid reviewId)
    {
        var review = await _context.Reviews.FindAsync(reviewId);
    
        if (review == null)
        {
            return NotFound("Review not found.");
        }

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null || review.ReviewerId != Guid.Parse(userId))
        {
            return Forbid("You are not authorized to delete this review.");
        }

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();

        return Ok("Review deleted successfully.");
    }

}