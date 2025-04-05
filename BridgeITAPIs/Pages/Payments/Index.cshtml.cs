using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BridgeITAPIs.services.Interface;
using System.Threading.Tasks;

public class PaymentsModel : PageModel
{
    private readonly IChargingService _chargingService;

    public PaymentsModel(IChargingService chargingService)
    {
        _chargingService = chargingService;
    }

    [BindProperty]
    public Guid ProposalId { get; set; }

    [BindProperty]
    public int Amount { get; set; }

    public string PaymentIntentId { get; set; }
    public string PaymentClientSecret { get; set; }

    // public async Task<IActionResult> OnPostAsync()
    // {
    //     var paymentIntent = await _chargingService.CreatePaymentIntentAsync(Amount, "acct_1Qvu4APIGUa5vpsj", "2d64bd7a-adb9-4a94-82c6-031838bd6682");
    //     PaymentIntentId = paymentIntent.Key;
    //     PaymentClientSecret = paymentIntent.Value;
    //     return Page();
    // }
}