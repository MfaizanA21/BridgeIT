using BridgeIT.Application.Abstractions.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BridgeIT.API.Controllers;

[ApiController]
[Route("api/bought-fyp")]
public class BoughtFypController : ControllerBase
{
    private readonly IBoughtFypService _service;

    public BoughtFypController(IBoughtFypService service)
    {
        _service = service;
    }

    [HttpGet()]
    public async Task<IActionResult> GetAllBoughtFyps()
    {
        var bfypl = await _service.GetAllBoughtFypsAsync();
        if (!bfypl.Any())
        {
            return NotFound("No bought Fyps found.");
        }

        return Ok(bfypl);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> BoughtFypById(Guid id)
    {
        var dto = await _service.GetBoughtFypByIdAsync(id);
        if (dto == null)
        {
            return NotFound("Bought FYP not found.");
        }

        return Ok(dto);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> AddAgreement(Guid id, [FromBody] string agreementDoc)
    {
        byte[] agreementBytes;
        try
        {
            agreementBytes = Convert.FromBase64String(agreementDoc);
        }
        catch
        {
            return BadRequest("Invalid base64 PDF data.");
        }

        var success = await _service.AddAgreementAsync(id, agreementBytes);
        if (!success)
        {
            return NotFound("Bought FYP not found.");
        }

        return Ok("Agreement added successfully.");
    }
}