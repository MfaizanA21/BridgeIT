using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BridgeITAPIs.DTOs.ProjectProposalDTOs;
using BridgeITAPIs.Models;

namespace BridgeITAPIs.Controllers;

[Route("api/project-proposals")]
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
            Status = "Pending",
            StudentId = dto.studentId,
            ProjectId = dto.projectId,
        };

        await _dbContext.Proposals.AddAsync(proposal);
        await _dbContext.SaveChangesAsync();

        return Ok("Proposal sent successfully.");
    }

    [HttpPut("reject-proposal/{ProposalId}")]
    public async Task<IActionResult> UpdateProposalStatus(Guid ProposalId)
    {
        var proposal = await _dbContext.Proposals
            .FirstOrDefaultAsync(p => p.Id == ProposalId);

        if (proposal == null)
        {
            return BadRequest("Proposal not found.");
        }

        proposal.Status = "Rejected";

        _dbContext.Proposals.Update(proposal);
        await _dbContext.SaveChangesAsync();

        return Ok("Proposal status Set To Rejected successfully.");
    }

    [HttpPut("accept-proposal/{ProposalId}")]
    public async Task<IActionResult> AcceptProposal(Guid ProposalId)
    {
        var proposal = await _dbContext.Proposals
            .FirstOrDefaultAsync(p => p.Id == ProposalId);

        if (proposal == null)
        {
            return BadRequest("Proposal not found.");
        }

        proposal.Status = "Accepted";

        _dbContext.Proposals.Update(proposal);
        await _dbContext.SaveChangesAsync();

        return Ok("Proposal status Set To Accepted successfully.");
    }

    [HttpGet("get-all-proposals")]
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

    [HttpGet("get-proposal-for-expert/{ExpertId}")]
    public async Task<IActionResult> GetProposalForExperts(Guid ExpertId)
    {
        var expert_proposals = await _dbContext.Proposals
            .Include(p => p.Student)
                .ThenInclude(p => p.University)
            .Include(p => p.Student)
                .ThenInclude(p => p.User)
            .Include(p => p.Project)
                .ThenInclude(p => p.IndExpert)
            .Where(p => p.Project.IndExpert.Id == ExpertId  && p.Status == "Pending")
            .ToListAsync();

        if (expert_proposals == null)
        {
            return BadRequest("No Proposals for your projects yet");
        }

        if (expert_proposals.Count == 0)
        {
            return BadRequest("No Proposals for your projects yet");
        }

        var proposalsList = expert_proposals.Select(p => new GetAllProposalDTO
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

        return Ok(proposalsList);

    }

    [HttpGet("get-proposal-by-id/{ProposalId}")]
    public async Task<IActionResult> GetProposalById(Guid ProposalId)
    {
        var proposal = await _dbContext.Proposals
            .Include(p => p.Student)
                .ThenInclude(s => s.University)
            .Include(p => p.Student)
                .ThenInclude(s => s.User)
            .Include(p => p.Project)
                .ThenInclude(i => i.IndExpert)
            .FirstOrDefaultAsync(p => p.Id == ProposalId);

        if (proposal == null)
        {
            return BadRequest("Proposal not found.");
        }

        var proposalDTO = new GetAllProposalDTO
        {
            Id = proposal.Id,
            ProjectId = proposal.ProjectId,
            StudentId = proposal.StudentId,
            ExpertId = proposal.Project?.IndExpert?.Id,
            StudentName = proposal?.Student?.User?.FirstName + " " + proposal?.Student?.User?.LastName ?? string.Empty,
            email = proposal?.Student?.User?.Email ?? string.Empty,
            Proposal = proposal.Proposal,
            Status = proposal.Status,
            skills = proposal.Student?.skills != null ? proposal.Student.skills.Split(',').ToList() : new List<string>(),
            university = proposal?.Student?.University?.Name ?? string.Empty,
            department = proposal?.Student?.department ?? string.Empty,
            description = proposal?.Student?.User?.description ?? string.Empty,
            ProjectTitle = proposal?.Project?.Title ?? string.Empty,
            ProjectDescription = proposal?.Project?.Description ?? string.Empty,
        };

        return Ok(proposalDTO);

    }


    [HttpGet("get-proposal-for-student/{StudentId}")]
    public async Task<IActionResult> GetProposalForStudent(Guid StudentId)
    {
        var student_proposals = await _dbContext.Proposals
            .Include(p => p.Student)
                .ThenInclude(p => p.University)
            .Include(p => p.Student)
                .ThenInclude(p => p.User)
            .Include(p => p.Project)
                .ThenInclude(p => p.IndExpert)
            .Where(p => p.StudentId == StudentId)
            .ToListAsync();

        if (student_proposals == null)
        {
            return BadRequest("No Proposals for your projects yet");
        }

        if (student_proposals.Count == 0)
        {
            return BadRequest("No Proposals for your projects yet");
        }

        var proposalsList = student_proposals.Select(p => new GetProposalsForStudentsDTO
        {
            Id = p.Id,
            ProjectId = p.ProjectId,
            StudentId = p.StudentId,
            ExpertId = p.Project?.IndExpert?.Id,
            StudentName = p?.Student?.User?.FirstName + " " + p?.Student?.User?.LastName ?? string.Empty,
            Proposal = p.Proposal,
            Status = p.Status,
            ProjectTitle = p?.Project?.Title ?? string.Empty,
            ProjectDescription = p?.Project?.Description ?? string.Empty,
        }).ToList();

        return Ok(proposalsList);

    }
    
}
