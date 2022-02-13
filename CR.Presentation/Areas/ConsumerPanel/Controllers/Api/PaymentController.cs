using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.RequestDTOs.Wallet;
using CR.Core.Services.Interfaces.FinancialTransaction;
using Microsoft.AspNetCore.Mvc;
using ServiceReference2;
using System;
using System.Threading.Tasks;

namespace CR.Presentation.Areas.ConsumerPanel.Controllers.Api
{
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IAddChargeWalletFinancialTransactionService _addChargeWalletFinancialTransactionService;
        private readonly IUpdateFinancialTransactionsRefIdService _updateFinancialTransactionsRefIdService;

        public PaymentController(IAddChargeWalletFinancialTransactionService addChargeWalletFinancialTransactionService
        , IUpdateFinancialTransactionsRefIdService updateFinancialTransactionsRefIdService)
        {
            _addChargeWalletFinancialTransactionService = addChargeWalletFinancialTransactionService;
            _updateFinancialTransactionsRefIdService = updateFinancialTransactionsRefIdService;
        }

        [Route("/api/Payment/ChargeWallet")]
        [HttpPost]
        public IActionResult ChargeWallet([FromForm] ChargeWalletPaymentDto model)
        {
            var payerId = ClaimUtility.GetUserId(User).Value;

            var result = _addChargeWalletFinancialTransactionService.Execute(payerId, model.price);

            if (result.IsSuccess == false)
            {
                return new JsonResult(new ResultDto<string>()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = result.Message
                });
            }

            if (result.Data.price == 0 || result.Data.transactionNumber == "0")
            {
                return new JsonResult(new ResultDto<string>()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "شماره تراکنش یا مبلغ قابل پرداخت نامعتبر است"
                });
            }

            long price = model.price * 10;

            var res = CallApi(price.ToString(), result.Data.transactionNumber);
            res.Wait();

            var output = res.Result.Body.@return;

            if (output.Split(",").Length >= 2)
            {
                var status = output.Split(",")[0];
                var refId = output.Split(",")[1];

                if (status == "0")
                {
                    _updateFinancialTransactionsRefIdService.Execute(result.Data.transactionNumber, refId);
                    string url = "https://bpm.shaparak.ir/pgwchannel/payment.mellat?RefId=" + refId;
                    return new JsonResult(new ResultDto<string>()
                    {
                        IsSuccess = true,
                        Message = string.Empty,
                        Data = url
                    });
                }
            }

            return new JsonResult(new ResultDto<string>()
            {
                IsSuccess = false,
                Message = string.Empty,
                Data = "/"
            });

        }

        private async Task<bpPayRequestResponse> CallApi(string price, string factorNumber)
        {
            try
            {
                var date = DateTime.Now;

                PaymentGatewayClient client = new PaymentGatewayClient(PaymentGatewayClient.EndpointConfiguration.PaymentGatewayImplPort);

                var result = await client.bpPayRequestAsync(
                    terminalId: 6240330,
                    userName: "choole777",
                    userPassword: "16020307",
                    orderId: Convert.ToInt32(factorNumber),
                    amount: Convert.ToInt32(price),
                    localDate: $"{date.Year}{date.Month}{date.Day}",
                    localTime: $"{date.Hour}{date.Minute}{date.Second}",
                    additionalData: "پس از نیم ساعت امکان لغو درخواست وجود ندارد",
                    callBackUrl: "http://www.chalechoole.com/ConsumerPanel/Payment/Verify",
                    payerId: "0",
                    mobileNo: "989122502978",
                    encPan: "",
                    panHiddenMode: "",
                    cartItem: "",
                    enc: ""
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
