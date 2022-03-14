using CR.Common.Utilities;
using CR.Core.Services.Interfaces.FinancialTransaction;
using CR.Core.Services.Interfaces.Wallet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.ConsumerPanel.Controllers.View
{
    [Authorize]
    [Area("ConsumerPanel")]
    public class FinancialTransactionsController : Controller
    {
        private readonly IGetConsumerFinancialTransactionsService _getConsumerFinancialTransactionsService;
        private readonly IGetWalletBalanceService _getWalletBalanceService;

        public FinancialTransactionsController(IGetConsumerFinancialTransactionsService getConsumerFinancialTransactionsService
        , IGetWalletBalanceService getWalletBalanceService)
        {
            _getConsumerFinancialTransactionsService = getConsumerFinancialTransactionsService;
            _getWalletBalanceService = getWalletBalanceService;
        }

        public IActionResult Index(int page = 1, int pageSize = 20)
        {
            var consumerId = ClaimUtility.GetUserId(User).Value;

            var model = _getConsumerFinancialTransactionsService.Execute(consumerId, page, pageSize).Data;

            model.WalletBalance = _getWalletBalanceService.Execute(consumerId).Data;

            return View(model);
        }
    }
}
