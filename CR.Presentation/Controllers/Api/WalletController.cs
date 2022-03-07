using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.RequestDTOs.ChatUser;
using CR.Core.DTOs.RequestDTOs.Wallet;
using CR.Core.Services.Interfaces.ChatUsers;
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
        private readonly IAddNewChatUserService _addNewChatUserService;

        public WalletController(IGetWalletBalanceService getWalletBalanceService
            , IAddPayFromWalletFinancialTransactionService addPayFromWalletFinancialTransactionService
            , IAddNewChatUserService addNewChatUserService)
        {
            _getWalletBalanceService = getWalletBalanceService;
            _addPayFromWalletFinancialTransactionService = addPayFromWalletFinancialTransactionService;
            _addNewChatUserService = addNewChatUserService;
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

            if (result.IsSuccess)
            {
                if (result.Data.IsChat)
                {
                    _addNewChatUserService.Execute(new RequestAddNewChatUserDto()
                    {
                        consumerId = result.Data.ConsumerId,
                        expertInformationId = result.Data.ExpertInformationId
                    });
                }
            }

            return new JsonResult(result);
        }
    }
}
