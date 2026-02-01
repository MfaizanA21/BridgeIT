using BridgeIT.API.DTOs.PaymentDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BridgeIT.Infrastructure;

namespace BridgeIT.API.Controllers;

[ApiController]
[Route("api/payment-details")]
public class PaymentDetailController : Controller
{
    private readonly BridgeItContext _dbContext;

    public PaymentDetailController(BridgeItContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpGet("payment-details")]
    public async Task<IActionResult> GetPaymentDetails()
    {
        var paymentDetails = await _dbContext.PaymentDetails
            .Include(p => p.Project)
            .ThenInclude(p => p!.Student)
            .ToListAsync();

        if (!paymentDetails.Any())
        {
            return NotFound("No payment details found for this project.");
        }

        return Ok(paymentDetails);
    }
    
    [HttpGet("payment-details/{id}")]
    public async Task<IActionResult> GetPaymentDetailById(Guid id)
    {
        var paymentDetail = await _dbContext.PaymentDetails
            .Include(p => p.Project)
            .ThenInclude(p => p!.Student)
            .ThenInclude(s => s!.User)
            .Include(p => p.Project)
            .ThenInclude(p => p!.IndExpert)
            .ThenInclude(i => i!.User)
            .FirstOrDefaultAsync(p => p.ProjectId == id || p.Id == id);

        if (paymentDetail == null)
        {
            return NotFound("No payment detail found for this project.");
        }
        
        var paymentDetailDTO = new GetPaymentDetailsDTO
        {
            Id = paymentDetail.Id,
            ProjectId = paymentDetail.ProjectId,
            StudentName = paymentDetail.Project!.Student!.User!.FirstName + " " + paymentDetail.Project.Student.User.LastName,
            ProjectOwnerName = paymentDetail.Project.IndExpert!.User!.FirstName + " " + paymentDetail.Project.IndExpert.User.LastName,
            ProjectName = paymentDetail.Project.Title!,
            StudentEmail = paymentDetail.Project.Student.User.Email!,
            StudentPaymentAccountId = paymentDetail.Project.Student.StripeConnectId!,
            PaidAt = paymentDetail.PaidAt,
            Receipt = paymentDetail.PaymentSlip,
            StudentId = paymentDetail.Project.Student.Id,
            ProjectOwnerId = paymentDetail.Project.IndExpert.Id,
        };

        return Ok(paymentDetailDTO);
    }
}