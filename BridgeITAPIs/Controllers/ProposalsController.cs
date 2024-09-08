using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BridgeITAPIs.DTOs.ProjectProposalDTOs;
using BridgeITAPIs.Models;

namespace BridgeITAPIs.Controllers;

[Route("api/Project-proposals")]
[ApiController]
public class ProposalsController : ControllerBase
{
    private readonly BridgeItContext _dbContext;

    public ProposalsController(BridgeItContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("send-proposal")]
    public async Task<IActionResult> SendProposal([FromBody] SendProposalDTO dto)
    {
        if (dto == null)
        {
            return BadRequest("Proposal Data is null.");
        }

        var proposal = new ProjectProposal
        {
            Id = Guid.NewGuid(),
            Proposal = dto.proposal,
            Status = dto.status,
            StudentId = dto.studentId,
            ProjectId = dto.projectId,
        };

        await _dbContext.Proposals.AddAsync(proposal);
        await _dbContext.SaveChangesAsync();

        return Ok("Proposal sent successfully.");
    }

    [HttpGet("get-proposals")]
    public async Task<IActionResult> GetProposals()
    {
        var proposals = await _dbContext.Proposals
            .Include(p => p.Student)
                .ThenInclude(s => s.University)
            .Include(p => p.Student)
                .ThenInclude(s => s.User)
            .Include(p => p.Project)
                .ThenInclude(i => i.IndExpert)
            .ToListAsync();

        var proposalDTOs = proposals.Select(p => new GetAllProposalDTO
        {
            Id = p.Id,
            ProjectId = p.ProjectId,
            StudentId = p.StudentId,
            ExpertId = p.Project?.IndExpert?.Id,
            StudentName = p?.Student?.User?.FirstName + " " + p?.Student?.User?.LastName ?? string.Empty,
            email = p?.Student?.User?.Email ?? string.Empty,
            Proposal = p.Proposal,
            Status = p.Status,
            skills = p.Student?.skills != null ? p.Student.skills.Split(',').ToList() : new List<string>(),
            university = p?.Student?.University?.Name ?? string.Empty,
            department = p?.Student?.department ?? string.Empty,
            description = p?.Student?.User?.description ?? string.Empty,
            ProjectTitle = p?.Project?.Title ?? string.Empty,
            ProjectDescription = p?.Project?.Description ?? string.Empty,

        }).ToList();

        return Ok(proposalDTOs);

    }
    
}
