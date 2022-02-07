using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.Payment;
using Microsoft.AspNetCore.Mvc;
using ServiceReference2;
using System;
using System.Threading.Tasks;

namespace CR.Presentation.Controllers.Api
{
    [ApiController]
    public class PaymentController : ControllerBase
    {
        [Route("/api/Payment/RedirectToPayment")]
        [HttpPost]
        public IActionResult RedirectToPayment(RedirectToPaymentDto model)
        {
            if (model.price == 0 || model.factorNumber == 0)
            {
                return new JsonResult(new ResultDto()
                {
                    IsSuccess = false,
                    Message = "شماره فاکتور یا مبلغ قابل پرداخت نامعتبر است"
                });
            }

            //var date = DateTime.Now;

            //var request = new RequestPaymentDto()
            //{
            //    amount = Convert.ToInt32(price),
            //    callBackUrl = "http://localhost:23065/Payment/Verify",
            //    orderId = Convert.ToInt32(factorNumber),
            //    terminalId = 6240330,
            //    userName = "choole777",
            //    userPassword = "16020307",
            //    localDate = $"{date.Year}{date.Month}{date.Day}",
            //    localTime = $"{date.Hour}{date.Minute}{date.Second}",
            //    payerId = ClaimUtility.GetUserId(User).Value,
            //    additionalData = "پس از نیم ساعت امکان لغو درخواست وجود ندارد"
            //};

            //var data = new
            //{
            //    request.amount,
            //    request.callBackUrl,
            //    request.terminalId,
            //    request.orderId,
            //    request.userName,
            //    request.userPassword,
            //    request.localDate,
            //    request.localTime,
            //    request.payerId,
            //    request.additionalData
            //};

            //var ipgUrl = "https://bpm.shaparak.ir/pgwchannel/services/pgw?wsdl";
            //var res = CallApi<object>(ipgUrl, data);
            //res.Wait();

            var res = CallApi(model.price.ToString(), model.factorNumber.ToString());
            res.Wait();

            var output = res.Result.Body.@return;

            if (output.Split().Length >= 2)
            {
                var status = output.Split(",")[0];
                var refId = output.Split(",")[1];

                if (status == "0")
                {
                    return new RedirectResult("https://pgw.bpm.bankmellat.ir/pgwchannel/startpay.mellat?RefId=" + refId);
                }
            }

            return new RedirectResult("/");

        }

        [Route("/api/Payment/Verify")]
        public IActionResult Verify(string refId, long saleOrderId)
        {
            return new JsonResult('s');
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
                    callBackUrl: "http://chalechoole.com/Payment/Verify",
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
//(DateTime.Now.Date.ToString(CultureInfo.InvariantCulture).Replace("/" , "")).Substring(0,8);
//var ipgUrl = "https://bpm.shaparak.ir/pgwchannel/startpay.mellat";