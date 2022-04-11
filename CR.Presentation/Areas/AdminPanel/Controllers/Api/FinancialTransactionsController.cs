using CR.Core.DTOs.RequestDTOs.FinancialTransactions;
using CR.Core.Services.Interfaces.FinancialTransaction;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers.Api
{
    [ApiController]
    public class FinancialTransactionsController : ControllerBase
    {
        private readonly IAddDeclineFinancialTransactionService _addDeclineFinancialTransactionService;

        public FinancialTransactionsController(IAddDeclineFinancialTransactionService addDeclineFinancialTransactionService)
        {
            _addDeclineFinancialTransactionService = addDeclineFinancialTransactionService;
        }

        [Route("/api/FinancialTransactions/Decline")]
        [HttpPost]
        public IActionResult Decline(RequestDeclineFinancialTransactionDto request)
        {
            var result = _addDeclineFinancialTransactionService.Execute(request.payerId, request.factorId);

            return new JsonResult(result);
        }
    }
}
