using CR.Common.Utilities;
using CR.Core.DTOs.RequestDTOs.FinancialTransactions;
using CR.Core.Services.Interfaces.FinancialTransaction;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.ExpertPanel.Controllers.Api
{
    [ApiController]
    public class FinancialTransactionsController : ControllerBase
    {
        private readonly IAddCheckoutFinancialTransactionService _addCheckoutFinancialTransactionService;
        private readonly IGetCheckoutFinancialTransactionDescriptionService _getCheckoutFinancialTransactionDescriptionService;

        public FinancialTransactionsController(IAddCheckoutFinancialTransactionService addCheckoutFinancialTransactionService
        , IGetCheckoutFinancialTransactionDescriptionService getCheckoutFinancialTransactionDescriptionService)
        {
            _addCheckoutFinancialTransactionService = addCheckoutFinancialTransactionService;
            _getCheckoutFinancialTransactionDescriptionService = getCheckoutFinancialTransactionDescriptionService;
        }

        [Route("/api/FinancialTransactions/RequestCheckout")]
        [HttpPost]
        public IActionResult RequestCheckout([FromForm] RequestCheckoutDto model)
        {
            var receiverId = ClaimUtility.GetUserId(User).Value;

            var result = _addCheckoutFinancialTransactionService.Execute(receiverId, model.price);

            return new JsonResult(result);
        }

        [Route("/api/CheckoutFinancialTransactions/GetDescriptionForExpert")]
        [HttpPost]
        public IActionResult GetDescriptionForExpert([FromBody] RequestGetCheckoutFinancialTransactionDescriptionDto model)
        {
            var result = _getCheckoutFinancialTransactionDescriptionService.Execute(model.id).Data;

            return new JsonResult(result);
        }
    }
}
