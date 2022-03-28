using CR.Common.ActiveMenus;
using CR.Core.Services.Interfaces.FinancialTransaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers.View
{
    [Area("AdminPanel")]
    [Authorize]
    public class CheckoutFinancialTransactionsController : Controller
    {
        private readonly IGetCheckoutFinancialTransactionsService _getCheckoutFinancialTransactionsService;

        public CheckoutFinancialTransactionsController(IGetCheckoutFinancialTransactionsService getCheckoutFinancialTransactionsService)
        {
            _getCheckoutFinancialTransactionsService = getCheckoutFinancialTransactionsService;
        }

        public IActionResult Index(int Page = 1, int PageSize = 20)
        {
            TempData["activemenu"] = ActiveMenu.CheckoutFinancialTransactions;

            var model = _getCheckoutFinancialTransactionsService.Execute(Page, PageSize).Data;

            return View(model);
        }
    }
}
