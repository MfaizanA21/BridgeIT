using BridgeITAPIs.services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BridgeITAPIs.Controllers;

[ApiController]
[Route("api/payments")]
public class PaymentController : Controller
{
    private readonly IChargingService _chargingServ;
    private readonly BridgeItContext _dbContext;

    public PaymentController(IChargingService chargingServ, BridgeItContext dbContext)
    {
        _chargingServ = chargingServ;
        _dbContext = dbContext;
    }
    
    [HttpPost("create-checkout-session")]
    public async Task<IActionResult> CreateCheckoutSession(Guid projectId)
    {
        var project = await _dbContext.Projects
            .FirstOrDefaultAsync(p => p.Id == projectId);

        if (project == null)
        {
            return NotFound("Proposal not found.");
        }

        // var project = proposal.Project;

        // Dummy success/cancel URLs for now (replace with frontend URLs)
        var successUrl = "https://your-frontend.com/payment-success";
        var cancelUrl = "https://your-frontend.com/payment-cancel";

        try
        {
            var checkoutUrl = await _chargingServ.CreateCheckoutSessionAsync(project.Budget ?? 20000, successUrl, cancelUrl, project.Id.ToString());
            return Ok(new { CheckoutUrl = checkoutUrl });
        }
        catch (Exception e)
        {
            return BadRequest(new { Error = "Failed to create checkout session", Details = e.Message });
        }
    }


    // [HttpPost("create-payment-intent")]
    // public async Task<IActionResult> CreatePaymentIntent(Guid ProposalId, int amount)
    // {
    //     var proposal = await _dbContext.Proposals
    //         .Include(p => p.Project)
    //         .Include(p => p.Student)
    //         .ThenInclude(s => s.User)
    //         .FirstOrDefaultAsync(p => p.Id == ProposalId);
    //
    //     if (proposal == null) {
    //         return NotFound("Proposal Not Found");
    //     }
    //
    //     var project = await _dbContext.Projects
    //             .Include(proj => proj.Proposals)
    //             .FirstOrDefaultAsync(p => p.Id == proposal.ProjectId);
    //
    //     if (project == null) { 
    //         return NotFound("Project Not Found");
    //     }
    //
    //     var student = await _dbContext.Students
    //         .Include(s => s.User)
    //         .FirstOrDefaultAsync(s => s.Id == proposal.StudentId);
    //
    //     if (student == null) {
    //         return NotFound("Student Not Found");
    //     }
    //
    //     if (string.IsNullOrEmpty(student.StripeConnectId))
    //     {
    //         return BadRequest("Student has no Stripe Connect ID.");
    //     }
    //
    //     try
    //     {
    //         var intent = await _chargingServ.CreatePaymentIntentAsync(amount, student.StripeConnectId, project.Id.ToString());
    //         return Ok(new { PaymentIntentId = intent.Key, PaymentClientSecret = intent.Value });
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(new { Error = "Failed to create charging intent.", Details = e.Message });
    //     }
    // }
    //
    // [HttpGet("payment-status/{paymentIntentId}")]
    // public async Task<IActionResult> GetPaymentIntentStatus(string paymentIntentId)
    // {
    //     if (string.IsNullOrEmpty(paymentIntentId))
    //     {
    //         return BadRequest("PaymentIntent ID is required.");
    //     }
    //
    //     try
    //     {
    //         // Fetch the payment intent status
    //         var status = await _chargingServ.GetPaymentIntentStatusAsync(paymentIntentId);
    //
    //         // Fetch payment intent details (including PaymentIntentId and PaymentClientSecret)
    //         var paymentIntentDetails = await _chargingServ.GetPaymentIntentDetailsAsync(paymentIntentId);
    //
    //         if (paymentIntentDetails == null)
    //         {
    //             return NotFound("Payment intent details not found.");
    //         }
    //
    //         return Ok(new
    //         {   
    //             PaymentIntentId = paymentIntentDetails.Value.PaymentIntentId,
    //             PaymentClientSecret = paymentIntentDetails.Value.PaymentClientSecret,
    //             Status = status
    //         });
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(new { Error = "Failed to retrieve payment status.", Details = e.Message });
    //     }
    // }



}
