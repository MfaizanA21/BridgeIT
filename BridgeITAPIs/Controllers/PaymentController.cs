using Microsoft.AspNetCore.Mvc;

namespace BridgeITAPIs.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
