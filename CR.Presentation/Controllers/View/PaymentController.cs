using CR.Core.Services.Interfaces.Factors;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Controllers.View
{
    public class PaymentController : Controller
    {
        private readonly IGetFactorDetailsService _getFactorDetailsService;

        public PaymentController(IGetFactorDetailsService getFactorDetailsService)
        {
            _getFactorDetailsService = getFactorDetailsService;
        }

        public IActionResult Index(string factorNumber)
        {
            var model = _getFactorDetailsService.Execute(factorNumber).Data;

            return View(model);
        }
    }
}
