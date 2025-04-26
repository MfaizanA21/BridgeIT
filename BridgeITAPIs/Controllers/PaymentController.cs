using BridgeITAPIs.DTOs.PaymentDTOs;
using BridgeITAPIs.services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;

namespace BridgeITAPIs.Controllers;

[ApiController]
[Route("api/payments")]
public class PaymentController : Controller
{
    private readonly IChargingService _chargingServ;
    private readonly BridgeItContext _dbContext;
    private readonly IPaymentSlipService _paymentSlipService;

    public PaymentController(IChargingService chargingServ, BridgeItContext dbContext, IPaymentSlipService paymentSlipService)
    {
        _chargingServ = chargingServ;
        _dbContext = dbContext;
        _paymentSlipService = paymentSlipService;
    }
    
    [HttpPost("create-checkout-session")]
    public async Task<IActionResult> CreateCheckoutSession(Guid projectId)
    {
        var project = await _dbContext.Projects
            .Include(s => s.Student)
            .FirstOrDefaultAsync(p => p.Id == projectId);

        if (project == null || project.Student == null)
        {
            return NotFound("Proposal not found.");
        }

        var stripeconnectId = project.Student.StripeConnectId;
        if(stripeconnectId == null)
        {
            return BadRequest("Student does not have a Stripe Connect account.");
        }
        
        var successUrl = "https://your-frontend.com/payment-success"; //Will Replace it eventually
        var cancelUrl = "https://your-frontend.com/payment-cancel"; //Will Replace it eventually

        try
        {
            var checkoutUrl = await _chargingServ.CreateCheckoutSessionAsync(project.Budget ?? 20000, successUrl, cancelUrl, project.Id.ToString(), stripeconnectId);
            return Ok(new { CheckoutUrl = checkoutUrl });
        }
        catch (Exception e)
        {
            return BadRequest(new { Error = "Failed to create checkout session", Details = e.Message });
        }
    }
    
    [HttpPost("webhook")]
    public async Task<IActionResult> StripeWebhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        Stripe.Event  stripeEvent;

        try
        {
            stripeEvent = EventUtility.ConstructEvent(
                json,
                Request.Headers["Stripe-Signature"],
                "your_stripe_webhook_secret_here"
            );
        }
        catch (Exception e)
        {
            return BadRequest(new { error = e.Message });
        }

        if (stripeEvent.Type == "checkout.session.completed")
        {
            var session = stripeEvent.Data.Object as Session;
            
            await HandlePaymentSuccess(session);
        }

        return Ok();
    }

    private async Task HandlePaymentSuccess(Session session)
    {
        var projectId = session.Metadata["project_id"];

        var project = await _dbContext.Projects
            .Include(s => s.Student)
            .ThenInclude(s => s!.User)
            .Include(c => c.IndExpert)
            .ThenInclude(s => s!.User)
            .FirstOrDefaultAsync(p => p.Id.ToString() == projectId);

        if (project == null) return;

        DateTime date = DateTime.UtcNow;

        var paymentSlipDto = new PaymentSlipDto
        {
            projecteeName = project.Student!.User!.FirstName + " " + project.Student.User.LastName,
            ClientName = project.IndExpert!.User!.FirstName + " " + project.IndExpert.User.LastName,
            ProjectTitle = project.Title!,
            PaymentDate = date,
            PaymentMethod = "Card",
            PaymentStatus = "Completed",
            PaymentAmount = $"{(project.Budget ?? 20000)} PKR",
            TransactionId = session.PaymentIntentId
        };

        var pdfBytes = _paymentSlipService.GeneratePaymentSlip(paymentSlipDto);
        
        await _dbContext.PaymentDetails.AddAsync(new PaymentDetail
        {
            Id = Guid.NewGuid(),
            ProjectId = project.Id,
            PaidAt = date,
            PaymentSlip = pdfBytes,
        });
        
        await _dbContext.SaveChangesAsync();
    }
}
