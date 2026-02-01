using BridgeIT.API.DTOs.PaymentDTOs;
using BridgeIT.API.services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;
using BridgeIT.Domain.Models;
using BridgeIT.Infrastructure;

namespace BridgeIT.API.Controllers;

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

    [HttpPost("create-checkout-session/fyp/{FypId}")]
    public async Task<IActionResult> CreateFypCheckoutSession(Guid FypId, [FromBody] FypPaymentDto fypPaymentDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid request data.");
        }
        
        var fyp = await _dbContext.Fyps
            .Include(f => f.Students)
            .FirstOrDefaultAsync(f => f.Id == FypId);
        if (fyp == null)
        {
            return NotFound("FYP not found.");
        }
        var stripeconnectId = fyp.Students.FirstOrDefault()?.StripeConnectId;
        
        if(stripeconnectId == null)
        {
            return BadRequest("Student does not have a Stripe Connect account.");
        }
        var metadata = new Dictionary<string, string>
        {
            { "fyp_id", fyp.Id.ToString() },
            { "payment_type", "fyp_marketplace" },
            {"indexpert_id", fypPaymentDto.IndExpertId.ToString()}
        };

        var successUrl = $"https://bridgeit-cyan.vercel.app/industryexpert/payment-complete?session_id={{CHECKOUT_SESSION_ID}}&project_id={FypId}";
        var cancelUrl = "https://bridgeit-cyan.vercel.app/industryexpert/payment-fail";
        try
        {
            var checkoutUrl = await _chargingServ.CreateCheckoutSessionAsync(fypPaymentDto.price, successUrl, cancelUrl, metadata, stripeconnectId);
            return Ok(new { CheckoutUrl = checkoutUrl });
        }
        catch (Exception e)
        {
            return BadRequest(new { Error = "Failed to create checkout session", Details = e.Message });
        }
    }
    
    [HttpPost("create-checkout-session/{projectId}")]
    public async Task<IActionResult> CreateCheckoutSession(Guid projectId)
    {
        var project = await _dbContext.Projects//
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
        
        var metadata = new Dictionary<string, string>
        {
            { "project_id", project.Id.ToString() },
            { "payment_type", "industry_project" }
        };
        
        var successUrl = $"https://bridgeit-cyan.vercel.app/industryexpert/payment-success?session_id={{CHECKOUT_SESSION_ID}}&project_id={projectId}" ; //Will Replace it eventually
        var cancelUrl = "https://bridgeit-cyan.vercel.app/industryexpert/payment-failure"; //Will Replace it eventually

        try
        {
            var checkoutUrl = await _chargingServ.CreateCheckoutSessionAsync(project.Budget ?? 20000, successUrl, cancelUrl, metadata, stripeconnectId);
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
                "whsec_n4KTNsaNNGg7Ut04cvzSbUnRE6ByO3xY"
            );
        }
        catch (Exception e)
        {
            return BadRequest(new { error = e.Message });
        }

        if (stripeEvent.Type == "checkout.session.completed")
        {
            var session = stripeEvent.Data.Object as Session;
            var paymentType = session?.Metadata["payment_type"];

            switch (paymentType)
            {
                case "industry_project":
                    await HandleIndustryProjectPayment(session);
                    break;
                case "fyp_marketplace":
                    await HandleFypPayment(session);
                    break;
                default:
                    // Optionally log unknown payment types
                    break;
            }
        }

        return Ok();
    }

    private async Task HandleFypPayment(Session session)
    {
        var fypId = session.Metadata["fyp_id"];
        var indExpertId = Guid.Parse(session.Metadata["indexpert_id"]);
        
        var fyp = await _dbContext.Fyps
            .Include(f => f.Students)
            .ThenInclude(s => s!.User)
            .Include(f => f.Faculty)
            .FirstOrDefaultAsync(f => f.Id.ToString() == fypId);
        
        if (fyp == null) return;
        
        var date = DateOnly.FromDateTime(DateTime.UtcNow);
        
        var uniAdmin = await _dbContext.UniversityAdmins
            .Include(u => u.User)
            .FirstOrDefaultAsync(u => u.UniId == fyp.Faculty!.UniId);

        await _dbContext.BoughtFyps.AddAsync(new BoughtFyp
        {
            Id = Guid.NewGuid(),
            FypId = fyp.Id,
            IndExpertId = indExpertId,
            UniversityAdminId = uniAdmin!.Id,
            // Agreement = agreementDoc,
            Price = long.Parse(session.AmountTotal.ToString()),
            PurchaseDate = date
        });

        await _dbContext.SaveChangesAsync();

    }
    private async Task HandleIndustryProjectPayment(Session session)
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

        project.CurrentStatus = "Completed";

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
