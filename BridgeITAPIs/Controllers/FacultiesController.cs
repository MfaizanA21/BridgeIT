using BridgeITAPIs.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BridgeITAPIs.Controllers;

[Route("api/faculties")]
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

    [HttpPatch("update-faculty/{Id}")]
    public async Task<IActionResult> UpdateFaculty(Guid Id, [FromBody] EditFacultyDTO dto)
    {
        var faculty = await _dbContext.Faculties
            .Include(f => f.User)
            .Include(f => f.Uni)
            .FirstOrDefaultAsync(f => f.UserId == Id);

        if (faculty == null)
        {
            return NotFound("Faculty not found.");
        }

        if (faculty.User != null)
        {
            faculty.User.FirstName = dto.FirstName;
            faculty.User.LastName = dto.LastName;
            faculty.User.Email = dto.Email;
            faculty.User.ImageData = dto.ImageData;
        }

        faculty.Interest = string.Join(",", dto.Interest);
        faculty.Post = dto.Post;

        if (faculty.Uni != null)
        {
            faculty.Uni.Id = dto.UniversityId.Value;
        }

        await _dbContext.SaveChangesAsync();

        return Ok("Faculty updated successfully.");
    }

    [HttpPut("update-faculty/{Id}")]
    public async Task<IActionResult> EditFaculty(Guid Id, [FromBody] EditFacultyDTO dto)
    {
        var faculty = await _dbContext.Faculties
            .Include(f => f.User)
            .Include(f => f.Uni)
            .FirstOrDefaultAsync(f => f.UserId == Id);

        if (faculty == null)
        {
            return NotFound("Faculty not found.");
        }

        if (faculty.User != null)
        {
            // Update User properties only if they are provided in the DTO
            if (!string.IsNullOrEmpty(dto.FirstName))
                faculty.User.FirstName = dto.FirstName;

            if (!string.IsNullOrEmpty(dto.LastName))
                faculty.User.LastName = dto.LastName;

            if (!string.IsNullOrEmpty(dto.Email))
                faculty.User.Email = dto.Email;

            if (dto.ImageData != null && dto.ImageData.Length > 0)
                faculty.User.ImageData = dto.ImageData;
        }

        if(dto.Interest != null && dto.Interest.Any())
            faculty.Interest = string.Join(",", dto.Interest);

        if(!string.IsNullOrEmpty(dto.Post))
            faculty.Post = dto.Post;

        if (faculty.Uni != null)
        {
            if (dto.UniversityId.HasValue)
                faculty.Uni.Id = dto.UniversityId.Value;
        }

        await _dbContext.SaveChangesAsync();

        return Ok("Faculty updated successfully.");
    }


}
