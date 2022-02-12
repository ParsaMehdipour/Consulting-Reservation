using CR.Common.Utilities;
using CR.Core.Services.Interfaces.FinancialTransaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.ConsumerPanel.Controllers
{
    [Authorize]
    [Area("ConsumerPanel")]
    public class FinancialTransactionsController : Controller
    {
        private readonly IGetConsumerFinancialTransactionsService _getConsumerFinancialTransactionsService;

        public FinancialTransactionsController(IGetConsumerFinancialTransactionsService getConsumerFinancialTransactionsService)
        {
            _getConsumerFinancialTransactionsService = getConsumerFinancialTransactionsService;
        }

        public IActionResult Index(int page = 1, int pageSize = 20)
        {
            var consumerId = ClaimUtility.GetUserId(User).Value;

            var model = _getConsumerFinancialTransactionsService.Execute(consumerId, page, pageSize).Data;

            return View(model);
        }
    }
}
