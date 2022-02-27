using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.Wallet;
using CR.Core.Services.Interfaces.Wallet;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Controllers.Api
{
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IGetWalletBalanceService _getWalletBalanceService;

        public WalletController(IGetWalletBalanceService getWalletBalanceService)
        {
            _getWalletBalanceService = getWalletBalanceService;
        }

        [Route("/api/Wallet/GetBalance")]
        [HttpPost]
        public ResultDto<int> GetBalance(RequestGetWalletBalanceDto request)
        {
            var balance = _getWalletBalanceService.Execute(request.payerId);

            return balance;
        }
    }
}
