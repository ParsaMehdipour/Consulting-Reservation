using CR.Core.DTOs.RequestDTOs.FinancialTransactions;
using CR.Core.Services.Interfaces.FinancialTransaction;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers.Api
{
    [ApiController]
    public class CheckoutFinancialTransactionsController : ControllerBase
    {
        private readonly IChangeCheckoutFinancialTransactionService _changeCheckoutFinancialTransactionService;
        private readonly IGetCheckoutFinancialTransactionDescriptionService _getCheckoutFinancialTransactionDescriptionService;

        public CheckoutFinancialTransactionsController(IChangeCheckoutFinancialTransactionService changeCheckoutFinancialTransactionService
        , IGetCheckoutFinancialTransactionDescriptionService getCheckoutFinancialTransactionDescriptionService)
        {
            _changeCheckoutFinancialTransactionService = changeCheckoutFinancialTransactionService;
            _getCheckoutFinancialTransactionDescriptionService = getCheckoutFinancialTransactionDescriptionService;
        }

        [Route("/api/CheckoutFinancialTransactions/ChangeStatus")]
        [HttpPost]
        public IActionResult ChangeStatus([FromBody] RequestChangeCheckoutFinancialTransactionStatus model)
        {
            var result = _changeCheckoutFinancialTransactionService.Execute(model.transactionId, model.transactionStatus, model.description);

            return new JsonResult(result);
        }

        [Route("/api/CheckoutFinancialTransactions/GetDescription")]
        [HttpPost]
        public IActionResult GetDescription([FromBody] RequestGetCheckoutFinancialTransactionDescriptionDto model)
        {
            var result = _getCheckoutFinancialTransactionDescriptionService.Execute(model.id).Data;

            return new JsonResult(result);
        }
    }
}
