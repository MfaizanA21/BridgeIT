using Microsoft.AspNetCore.Mvc;
using BridgeITAPIs.DTOs.PaymentDTOs;
using BridgeITAPIs.Helper;
using Stripe;

namespace BridgeITAPIs.Controllers;

[Route("api/payments")]
[ApiController]
public class PaymentController : Controller
{
    private readonly PaymentService _paymentService;

    public PaymentController(PaymentService paymentService)
    {
        _paymentService = paymentService;
    }
    
    [HttpPost("create-student-account")]
    public async Task<IActionResult> CreateStudentAccount([FromBody] CreateAccountDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.FirstName) || string.IsNullOrWhiteSpace(dto.LastName))
        {
            return BadRequest("Invalid input.");
        }

        try
        {
            var accountId = await _paymentService.CreateStudentAccount(dto.Email, dto.FirstName, dto.LastName);
            return Ok(new { AccountId = accountId });
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("create-payment-intent")]
    public async Task<IActionResult> CreatePaymentIntent([FromBody] CreatePaymentIntentDto dto)
    {
        if (dto.Amount <= 0 || string.IsNullOrWhiteSpace(dto.StudentPaymentAccountId) || string.IsNullOrWhiteSpace(dto.ProjectId))
        {
            return BadRequest("Invalid input.");
        }

        try
        {
            var paymentIntent = await _paymentService.CreatePaymentIntentAsync(dto.Amount, dto.StudentPaymentAccountId, dto.ProjectId);
            return Ok(paymentIntent);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}