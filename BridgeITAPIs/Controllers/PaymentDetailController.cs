using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BridgeITAPIs.Controllers;

[ApiController]
[Route("api/payment-details")]
public class PaymentDetailController : Controller
{
    private readonly BridgeItContext _dbContext;

    public PaymentDetailController(BridgeItContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpGet("get-payment-details")]
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
}