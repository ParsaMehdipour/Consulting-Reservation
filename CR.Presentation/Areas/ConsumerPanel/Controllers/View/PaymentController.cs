using CR.Common.DTOs;
using CR.Core.Services.Interfaces.FinancialTransaction;
using CR.DataAccess.Enums;
using Microsoft.AspNetCore.Mvc;
using ServiceReference2;
using System;
using System.Threading.Tasks;

namespace CR.Presentation.Areas.ConsumerPanel.Controllers
{
    [Area("ConsumerPanel")]
    public class PaymentController : Controller
    {
        private readonly IUpdateFinancialTransactionSaleReferenceIdService
            _updateFinancialTransactionSaleReferenceIdService;

        private readonly IUpdateFinancialTransactionCarHolderPANService _updateFinancialTransactionCarHolderPanService;
        private readonly IUpdateFinancialTransactionStatusService _updateFinancialTransactionStatusService;
        private readonly IGetFinancialTransactionForVerifyService _getFinancialTransactionForVerifyService;

        public PaymentController(IGetFinancialTransactionForVerifyService getFinancialTransactionForVerifyService
            , IUpdateFinancialTransactionSaleReferenceIdService updateFinancialTransactionSaleReferenceIdService
            , IUpdateFinancialTransactionCarHolderPANService updateFinancialTransactionCarHolderPanService
            , IUpdateFinancialTransactionStatusService updateFinancialTransactionStatusService)
        {
            _updateFinancialTransactionSaleReferenceIdService = updateFinancialTransactionSaleReferenceIdService;
            _updateFinancialTransactionCarHolderPanService = updateFinancialTransactionCarHolderPanService;
            _updateFinancialTransactionStatusService = updateFinancialTransactionStatusService;
            _getFinancialTransactionForVerifyService = getFinancialTransactionForVerifyService;
        }

        [HttpPost]
        public IActionResult Verify(string RefId, string ResCode, long SaleOrderId, long SaleReferenceId, string CardHolderPan, long FinalAmount)
        {
            var result = new ResultDto()
            {
                IsSuccess = false
            };

            if (ResCode == "0")
            {
                var transaction = _getFinancialTransactionForVerifyService.Execute(SaleOrderId.ToString()).Data;

                if (transaction == null)
                {
                    ViewData["Description"] = "تراکنش معتبر نمی باشد!!";
                    return View(result);
                }

                if (transaction.RefId != RefId)
                {
                    ViewData["Description"] = "تراکنش معتبر نمی باشد";
                    return View(result);
                }

                if (transaction.Price != FinalAmount / 10)
                {
                    ViewData["Description"] = "تراکنش معتبر نمی باشد";
                    return View(result);
                }

                _updateFinancialTransactionSaleReferenceIdService.Execute(SaleOrderId.ToString(), SaleReferenceId);

                var res = CallApi(SaleOrderId.ToString(), SaleReferenceId);

                res.Wait();

                var resCode = res.Result.Body.@return;

                if (resCode == "0")
                {
                    _updateFinancialTransactionCarHolderPanService.Execute(SaleOrderId.ToString(), CardHolderPan);

                    _updateFinancialTransactionStatusService.Execute(SaleOrderId.ToString(),
                        TransactionStatus.Successful);

                    ViewData["Description"] = "تراکنش با موفقیت انجام شد، کد رهگیری پرداخت شما : " + SaleReferenceId;

                    result.IsSuccess = true;

                    return View(result);
                }

                ViewData["Description"] = "تراکنش ناموفق";

                ViewData["ResCode"] = resCode;

                _updateFinancialTransactionStatusService.Execute(SaleOrderId.ToString(), TransactionStatus.Failed);

                return View(result);
            }

            ViewData["Description"] = "پرداخت ناموفق";

            _updateFinancialTransactionStatusService.Execute(SaleOrderId.ToString(), TransactionStatus.Failed);

            ViewData["ResCode"] = ResCode;

            return View(result);
        }

        private async Task<bpVerifyRequestResponse> CallApi(string factorNumber, long saleReferenceId)
        {
            try
            {
                var date = DateTime.Now;

                PaymentGatewayClient client =
                    new PaymentGatewayClient(PaymentGatewayClient.EndpointConfiguration.PaymentGatewayImplPort);

                var result = await client.bpVerifyRequestAsync(
                    terminalId: 6240330,
                    userName: "choole777",
                    userPassword: "16020307",
                    orderId: Convert.ToInt32(factorNumber),
                    saleOrderId: Convert.ToInt32(factorNumber),
                    saleReferenceId: saleReferenceId
                );

                return result;

            }
            catch (Exception e)
            {
                var exception = e.Message;

                return null;
            }
        }
    }
}
