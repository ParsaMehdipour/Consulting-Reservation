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

        public FinancialTransactionsController(IAddCheckoutFinancialTransactionService addCheckoutFinancialTransactionService)
        {
            _addCheckoutFinancialTransactionService = addCheckoutFinancialTransactionService;
        }

        [Route("/api/FinancialTransactions/RequestCheckout")]
        [HttpPost]
        public IActionResult RequestCheckout([FromForm] RequestCheckoutDto model)
        {
            var receiverId = ClaimUtility.GetUserId(User).Value;

            var result = _addCheckoutFinancialTransactionService.Execute(receiverId, model.price);

            return new JsonResult(result);
        }
    }
}
