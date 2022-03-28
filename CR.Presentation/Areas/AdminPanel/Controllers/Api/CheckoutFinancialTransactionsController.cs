using CR.Core.DTOs.RequestDTOs.FinancialTransactions;
using CR.Core.Services.Interfaces.FinancialTransaction;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers.Api
{
    [ApiController]
    public class CheckoutFinancialTransactionsController : ControllerBase
    {
        private readonly IChangeCheckoutFinancialTransactionService _changeCheckoutFinancialTransactionService;

        public CheckoutFinancialTransactionsController(IChangeCheckoutFinancialTransactionService changeCheckoutFinancialTransactionService)
        {
            _changeCheckoutFinancialTransactionService = changeCheckoutFinancialTransactionService;
        }

        [Route("/api/CheckoutFinancialTransactions/ChangeStatus")]
        [HttpPost]
        public IActionResult ChangeStatus([FromBody] RequestChangeCheckoutFinancialTransactionStatus model)
        {
            var result = _changeCheckoutFinancialTransactionService.Execute(model.transactionId, model.transactionStatus);

            return new JsonResult(result);
        }
    }
}
