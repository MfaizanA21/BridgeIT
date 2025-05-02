using BridgeITAPIs.DTOs.EducationalResourceDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BridgeITAPIs.Controllers;

[Route("api/educational-resources")]
[ApiController]
public class EducationalResourceController : ControllerBase
{
    private readonly BridgeItContext _dbContext;
    
    public EducationalResourceController(BridgeItContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllEducationalResources()
    {
        var resources = await _dbContext.Set<EductionalResource>().ToListAsync();
        return Ok(resources);
    }
    
    [HttpPost("add")]
    public async Task<IActionResult> AddEducationalResource([FromBody] AddEducationalResourceDTO resource)
    {
        
        if (resource.FacultyId == Guid.Empty)
        {
            return BadRequest("Faculty id can never be null");
        }
        var fId = await _dbContext.Faculties
            .SingleOrDefaultAsync(f => f.Id == resource.FacultyId);

        if (fId == null)
        {
            return NotFound("Faculty not found against this id");
        }
        
        var newResource = new EductionalResource
        {
            Id = Guid.NewGuid(),
            Title = resource.Title,
            Content = resource.Content,
            SourceLink = resource.SourceLink,
            FacultyId = resource.FacultyId
        };
        
        await _dbContext.EductionalResources.AddAsync(newResource);
        await _dbContext.SaveChangesAsync();
        return Ok("Educational Resource Added Successfully!");
    }
    
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteEducationalResource(Guid id)
    {
        var resource = await _dbContext.EductionalResources.FindAsync(id);
        if (resource == null)
        {
            return NotFound("Educational Resource not found");
        }
        
        _dbContext.EductionalResources.Remove(resource);
        await _dbContext.SaveChangesAsync();
        return Ok("Educational Resource Deleted Successfully!");
    }
    
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateEducationalResource(Guid id, [FromBody] EditEducationalResourceDTO resource)
    {
        var existingResource = await _dbContext.EductionalResources.FindAsync(id);
        if (existingResource == null)
        {
            return NotFound("Educational Resource not found");
        }

        if (!string.IsNullOrWhiteSpace(resource.Title))
        {
            existingResource.Title = resource.Title;
        }
        if (!string.IsNullOrWhiteSpace(resource.Content))
        {
            existingResource.Content = resource.Content;
        }
        if (!string.IsNullOrWhiteSpace(resource.SourceLink))
        {
            existingResource.SourceLink = resource.SourceLink;
        }

        await _dbContext.SaveChangesAsync();
        return Ok("Educational Resource Updated Successfully!");
    }
 
    [HttpGet("get-by-id/{id}")]
    public async Task<IActionResult> GetEducationalResourcesByFaculty(Guid id)
    {
        var resources = await _dbContext.EductionalResources
            .Include(e => e.Faculty)
            .ThenInclude(f => f!.User)
            .Include(e => e.Faculty)
            .ThenInclude(f => f!.Uni)
            .Where(r => r.FacultyId == id || r.Faculty!.UniId == id || r.Id == id)
            .ToListAsync();
        
        if (!resources.Any())
        {
            return NotFound("No Educational Resources found against this id");
        }

        var resourceDto = resources
            .Select(r => new GetEducationalRedourcesDTO
        {
            id = r.Id,
            title = r.Title,
            content = r.Content,
            sourceLink = r.SourceLink,
            facultyId = r.FacultyId,
            facultyName = r.Faculty!.User!.FirstName + " " + r.Faculty.User.LastName,
            facultyPost = r.Faculty.Post ?? string.Empty,
            facultyDepartment = r.Faculty.Department!,
            universityId = r.Faculty.Uni!.Id,
            universityName = r.Faculty.Uni.Name!,
            universityLocation = r.Faculty.Uni.Address ?? String.Empty
        }).ToList();
        
        return Ok(resourceDto);
    }
}