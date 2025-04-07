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

}
