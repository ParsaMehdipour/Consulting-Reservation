using CR.Common.Utilities;
using CR.Core.Services.Interfaces.FinancialTransaction;
using CR.Core.Services.Interfaces.Wallet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.ExpertPanel.Controllers.View
{
    [Area("ExpertPanel")]
    [Authorize]
    public class FinancialTransactionsController : Controller
    {
        private readonly IGetExpertFinancialTransactionService _getExpertFinancialTransactionService;
        private readonly IGetExpertWalletBalanceService _getExpertWalletBalanceService;

        public FinancialTransactionsController(IGetExpertFinancialTransactionService getExpertFinancialTransactionService
        , IGetExpertWalletBalanceService getExpertWalletBalanceService)
        {
            _getExpertFinancialTransactionService = getExpertFinancialTransactionService;
            _getExpertWalletBalanceService = getExpertWalletBalanceService;
        }

        public IActionResult Index(int page = 1, int pageSize = 20)
        {
            var expertId = ClaimUtility.GetUserId(User).Value;

            var model = _getExpertFinancialTransactionService.Execute(expertId, page, pageSize).Data;
            model.WalletBalance = _getExpertWalletBalanceService.Execute(expertId).Data;

            return View(model);
        }
    }
}
