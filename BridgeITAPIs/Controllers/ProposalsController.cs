using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BridgeITAPIs.DTOs.ProjectProposalDTOs;
using BridgeITAPIs.Models;
using BridgeITAPIs.services.Interface;

namespace BridgeITAPIs.Controllers;

[Route("api/project-proposals")]
[ApiController]
public class ProposalsController : ControllerBase
{
    private readonly BridgeItContext _dbContext;
    private readonly MailService _mailService;
    private readonly IChargingService _chargingServ;
    private readonly ILogger<ProposalsController> _logger;

    public ProposalsController(BridgeItContext dbContext, MailService mailService, IChargingService chargingServ, ILogger<ProposalsController> logger)
    {
        _dbContext = dbContext;
        _mailService = mailService;
        _chargingServ = chargingServ;
        _logger = logger;
    }

    [HttpPost("send-proposal")]
    public async Task<IActionResult> SendProposal([FromBody] SendProposalDTO dto)
    {
        var proj = await _dbContext.Projects.FirstOrDefaultAsync(p => p.Id == dto.projectId);
        var student = await _dbContext.Students.Include(u => u.User).FirstOrDefaultAsync(s => s.Id == dto.studentId);

        if (proj == null || student == null)
        {
            return BadRequest("Project not found.");
        }
        
        if (string.IsNullOrEmpty(dto.proposal) || dto.proposal.Length == 0)
        {
            return BadRequest("Proposal file is required.");
        }

        try
        {
            var proposalBytes = Convert.FromBase64String(dto.proposal);

            var proposal = new ProjectProposal
            {
                Id = Guid.NewGuid(),
                Proposal = proposalBytes,
                Status = "Pending",
                StudentId = dto.studentId,
                ProjectId = dto.projectId,
            };
            await _dbContext.Proposals.AddAsync(proposal);
            await _dbContext.SaveChangesAsync();

            await _mailService.SendProjectProposalMail(student.User.Email, proj.Title);
            
            return Ok("Proposal sent successfully.");
            
        } catch (FormatException)
        {
            return BadRequest("Invalid base64 string.");
        }
        
        // using var memoryStream = new MemoryStream();
        // await dto.proposal.CopyToAsync(memoryStream);
        //
        // var proposal = new ProjectProposal
        // {
        //     Id = Guid.NewGuid(),
        //     Proposal = memoryStream.ToArray(),
        //     Status = "Pending",
        //     StudentId = dto.studentId,
        //     ProjectId = dto.projectId,
        // };
        //
        // await _dbContext.Proposals.AddAsync(proposal);
        // await _dbContext.SaveChangesAsync();
        //
        // return Ok("Proposal sent successfully.");
    }

    [HttpGet("check-sent-proposal/{StudentId}/{ProjectId}")]
    public async Task<IActionResult> CheckSentProposal(Guid StudentId, Guid ProjectId)
    {
        var proposal = await _dbContext.Proposals
            .FirstOrDefaultAsync(p => p.StudentId == StudentId && p.ProjectId == ProjectId);

        if (proposal == null)
        {
            return BadRequest("No proposal found.");
        }

        return Ok("Proposal found.");
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
        
        var student = await _dbContext.Students
            .Include(u => u.User)
            .FirstOrDefaultAsync(s => s.Id == proposal.StudentId);

        var project = await _dbContext.Projects
            .FirstOrDefaultAsync(p => p.Id == proposal.ProjectId);

        if (student == null || project == null)
        {
            return BadRequest("Student or Project not found.");
        }
        
        proposal.Status = "Rejected";

        _dbContext.Proposals.Update(proposal);
        await _dbContext.SaveChangesAsync();

        await _mailService.ProjectProposalStatusMail(student.User.Email, project.Title, proposal.Status);

        return Ok("Proposal status Set To Rejected successfully.");
    }

    [HttpPut("accept-proposal/{ProposalId}")]
    public async Task<IActionResult> AcceptProposal(Guid ProposalId)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            // Fetch the proposal with necessary includes
            var proposal = await _dbContext.Proposals
                .Include(p => p.Project)
                .Include(p => p.Student)
                .ThenInclude(s => s.User)
                .FirstOrDefaultAsync(p => p.Id == ProposalId);

            if (proposal == null)
            {
                return BadRequest("Proposal not found.");
            }

            // Check if the proposal is already accepted
            if (proposal.Status == "Accepted")
            {
                return BadRequest("Proposal has already been accepted.");
            }

            // Fetch the project with proposals
            var project = await _dbContext.Projects
                .Include(proj => proj.Proposals)
                .FirstOrDefaultAsync(p => p.Id == proposal.ProjectId);

            if (project == null)
            {
                return BadRequest("Associated project not found.");
            }

            // Fetch the student with user details
            var student = await _dbContext.Students
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.Id == proposal.StudentId);

            if (student == null)
            {
                return BadRequest("Associated student not found.");
            }

            // Attempt to create Stripe payment intent before making any database changes
            //string paymentClientSecret;
            //string paymentIntentId;

            //if (string.IsNullOrEmpty(student.StripeConnectId))
            //{
            //    return BadRequest("Student has no Stripe Connect ID.");
            //}

            //try
            //{
            //    var intent = await _chargingServ.CreatePaymentIntentAsync(5000, student.StripeConnectId, project.Id.ToString());
            //    paymentIntentId = intent.Key;
            //    paymentClientSecret = intent.Value;
            //}
            //catch (Exception e)
            //{
            //    _logger.LogError("Failed to create charging intent for project {projectId}: {ErrorMessage}", project.Id, e.Message);
            //    return BadRequest(new { Error = "Failed to create charging intent.", Details = e.Message });
            //}
            
            //var paymentIntentStatus = await _chargingServ.GetPaymentIntentStatusAsync(paymentIntentId);
            //if (paymentIntentStatus != "succeeded")
            //{
            //    return BadRequest("Payment failed or not completed. Proposal not accepted.");
            //}

            // Update proposal and project after successful Stripe payment intent creation
            proposal.Status = "Accepted";
            proposal.Project.StudentId = proposal.StudentId;
            //proposal.PaymentIntentId = paymentIntentId;

            // Reject other proposals associated with the project
            var otherProposals = project.Proposals.Where(p => p.Id != ProposalId && p.Status != "Rejected").ToList();

            foreach (var prop in otherProposals)
            {
                prop.Status = "Rejected";
                _dbContext.Proposals.Update(prop);

                // Send rejection email asynchronously
                if (prop.Student?.User?.Email != null)
                {
                    await _mailService.ProjectProposalStatusMail(prop.Student.User.Email, project.Title, prop.Status);
                }
            }

            // Update the accepted proposal
            _dbContext.Proposals.Update(proposal);

            // Save all changes within the transaction
            await _dbContext.SaveChangesAsync();

            // Commit the transaction
            await transaction.CommitAsync();

            // Send acceptance email
            if (student.User?.Email != null)
            {
                await _mailService.ProjectProposalStatusMail(student.User.Email, project.Title, proposal.Status);
            }

            return Ok(new
            {
                Message = "Proposal status set to Accepted successfully.",
                //PaymentClientSecret = paymentClientSecret
            });
        }
        catch (Exception ex)
        {
            // Rollback the transaction in case of any failure
            await transaction.RollbackAsync();

            _logger.LogError("An error occurred while accepting the proposal {ProposalId}: {ErrorMessage}", ProposalId, ex.Message);
            return StatusCode(500, "An unexpected error occurred while processing your request.");
        }
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
        
        if (proposals == null)
        {
            return BadRequest("No Proposals found.");
        }

        var proposalDTOs = proposals.Select(p => new GetAllProposalDTO
        {
            Id = p.Id,
            ProjectId = p.ProjectId,
            StudentId = p.StudentId,
            ExpertId = p.Project?.IndExpert?.Id,
            StudentName = (p.Student?.User?.FirstName ?? String.Empty) + " " + (p.Student?.User?.LastName ?? string.Empty),
            email = p?.Student?.User?.Email ?? string.Empty,
            Proposal = p.Proposal != null ? Convert.ToBase64String(p.Proposal) : string.Empty,
            Status = p.Status,
            skills = p.Student?.skills != null ? p.Student.skills.Split(',').ToList() : new List<string>(),
            university = p?.Student?.University?.Name ?? string.Empty,
            department = p?.Student?.department ?? string.Empty,
            description = p?.Student?.User?.description ?? string.Empty,
            ProjectTitle = p?.Project?.Title ?? string.Empty,
            ProjectDescription = p?.Project?.Description ?? string.Empty,

        }).ToList();

        if (proposalDTOs.Count == 0)
        {
            return BadRequest("No Proposals found.");
        }
        
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

        if (!expert_proposals.Any() || expert_proposals.Count == 0)
        {
            return BadRequest("No Proposals for your projects yet");
        }

        // if (expert_proposals.Count == 0)
        // {
        //     return BadRequest("No Proposals for your projects yet");
        // }

        var proposalsList = expert_proposals.Select(p => new GetAllProposalDTO
        {
            Id = p.Id,
            ProjectId = p.ProjectId,
            StudentId = p.StudentId,
            ExpertId = p.Project?.IndExpert?.Id,
            StudentName = p?.Student?.User?.FirstName + " " + p?.Student?.User?.LastName ?? string.Empty,
            email = p?.Student?.User?.Email ?? string.Empty,
            Proposal = p.Proposal != null ? Convert.ToBase64String(p.Proposal) : string.Empty,
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
            Proposal = proposal.Proposal != null ? Convert.ToBase64String(proposal.Proposal) : string.Empty,
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

        if (!student_proposals.Any())
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
            Proposal = p?.Proposal != null ? Convert.ToBase64String(p.Proposal) : string.Empty,
            Status = p.Status,
            ProjectTitle = p?.Project?.Title ?? string.Empty,
            ProjectDescription = p?.Project?.Description ?? string.Empty,
        }).ToList();

        return Ok(proposalsList);

    }
    
    /// <summary>
    /// This is a demo api. please edit it for your use case. When project is completed. release payment
    /// </summary>
    /// <param name="proposalId"></param>
    /// <returns></returns>
    [HttpPost("{proposalId}/complete")]
    public async Task<IActionResult> CompleteProject(Guid proposalId)
    {
        var proposal = await _dbContext.Proposals
            .Include(p => p.Project)
            .FirstOrDefaultAsync(p => p.Id == proposalId);

        if (proposal == null)
        {
            return BadRequest("Proposal not found.");
        }

        if (proposal.PaymentIntentId == null)
        {
            return BadRequest("Payment not made.");
        }

        await _chargingServ.ReleasePayment(proposal.PaymentIntentId);

        proposal.Project.CurrentStatus = "Completed";
        
        return Ok("Project Completed.");
    }
}
