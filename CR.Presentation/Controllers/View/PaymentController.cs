using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Controllers.View
{
    public class PaymentController : Controller
    {
        public IActionResult Index(string factorNumber)
        {
            return View();
        }
    }
}
