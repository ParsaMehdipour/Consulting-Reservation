using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.Payment;
using CR.Core.Services.Interfaces.Factors;
using CR.Core.Services.Interfaces.FinancialTransaction;
using Microsoft.AspNetCore.Mvc;
using ServiceReference2;
using System;
using System.Threading.Tasks;

namespace CR.Presentation.Controllers.Api
{
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IUpdateFactorRefIdService _updateFactorRefIdService;
        private readonly IAddPaymentTransactionService _addPaymentTransactionService;

        public PaymentController(IUpdateFactorRefIdService updateFactorRefIdService
        , IAddPaymentTransactionService addPaymentTransactionService)
        {
            _updateFactorRefIdService = updateFactorRefIdService;
            _addPaymentTransactionService = addPaymentTransactionService;
        }

        [Route("/api/Payment/RedirectToPayment")]
        [HttpPost]
        public IActionResult RedirectToPayment(RedirectToPaymentDto model)
        {
            if (model.price == 0 || model.factorId == 0)
            {
                return new JsonResult(new ResultDto<string>()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "شماره فاکتور یا مبلغ قابل پرداخت نامعتبر است"
                });
            }

            var result = _addPaymentTransactionService.Execute(model.factorId, model.price);

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

            int price = result.Data.price * 10;

            var res = CallApi(price.ToString(), result.Data.transactionNumber, result.Data.phoneNumber);

            res.Wait();

            var output = res.Result.Body.@return;

            if (output.Split(",").Length >= 2)
            {
                var status = output.Split(",")[0];
                var refId = output.Split(",")[1];

                if (status == "0")
                {
                    _updateFactorRefIdService.Execute(model.factorId, refId);
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
                IsSuccess = true,
                Message = string.Empty,
                Data = "/"
            });

        }

        private async Task<bpPayRequestResponse> CallApi(string price, string factorNumber, string phoneNumber)
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
                    callBackUrl: "https://www.chalechoole.com/Payment/Verify",
                    payerId: "0",
                    mobileNo: phoneNumber,
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