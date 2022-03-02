using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.RequestDTOs.Wallet;
using CR.Core.Services.Interfaces.FinancialTransaction;
using CR.Core.Services.Interfaces.Wallet;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Controllers.Api
{
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IGetWalletBalanceService _getWalletBalanceService;
        private readonly IAddPayFromWalletFinancialTransactionService _addPayFromWalletFinancialTransactionService;

        public WalletController(IGetWalletBalanceService getWalletBalanceService
            , IAddPayFromWalletFinancialTransactionService addPayFromWalletFinancialTransactionService)
        {
            _getWalletBalanceService = getWalletBalanceService;
            _addPayFromWalletFinancialTransactionService = addPayFromWalletFinancialTransactionService;
        }

        [Route("/api/Wallet/GetBalance")]
        [HttpPost]
        public ResultDto<int> GetBalance(RequestGetWalletBalanceDto request)
        {
            var balance = _getWalletBalanceService.Execute(request.payerId);

            return balance;
        }

        [Route("/api/Wallet/Pay")]
        [HttpPost]
        public IActionResult Pay(RequestPayFromWalletDto request)
        {
            var payerId = ClaimUtility.GetUserId(User).Value;

            var result = _addPayFromWalletFinancialTransactionService.Execute(payerId, request.factorId, request.price);

            return new JsonResult(result);
        }
    }
}
