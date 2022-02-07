using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.Payment;
using CR.Core.Services.Interfaces.Factors;
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

        public PaymentController(IUpdateFactorRefIdService updateFactorRefIdService)
        {
            _updateFactorRefIdService = updateFactorRefIdService;
        }

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

            var res = CallApi(model.price.ToString(), model.factorNumber.ToString());
            res.Wait();

            var output = res.Result.Body.@return;

            if (output.Split().Length >= 2)
            {
                var status = output.Split(",")[0];
                var refId = output.Split(",")[1];

                if (status == "0")
                {
                    _updateFactorRefIdService.Execute(model.factorNumber.ToString(), refId);
                    return new RedirectResult("https://pgw.bpm.bankmellat.ir/pgwchannel/startpay.mellat?RefId=" + refId);
                }
            }

            return new RedirectResult("/");

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