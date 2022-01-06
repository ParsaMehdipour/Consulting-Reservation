using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Payment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace CR.Presentation.Controllers.Api
{
    [ApiController]
    public class PaymentController : ControllerBase
    {
        [Route("/api/Payment/RedirectToPayment")]
        [HttpPost]
        public IActionResult RedirectToPayment(string price = "1000", string factorNumber = "1")
        {
            if (string.IsNullOrEmpty(price) || string.IsNullOrEmpty(factorNumber))
            {
                return new JsonResult(new ResultDto()
                {
                    IsSuccess = false,
                    Message = "شماره فاکتور یا مبلغ قابل پرداخت نامعتبر است"
                });
            }

            var date = DateTime.Now;

            var request = new RequestPaymentDto()
            {
                amount = Convert.ToInt32(price),
                callBackUrl = "http://localhost:23065/Payment/Verify",
                orderId = Convert.ToInt32(factorNumber),
                terminalId = 6240330,
                userName = "choole777",
                userPassword = "16020307",
                localDate = $"{date.Year}{date.Month}{date.Day}",
                localTime = $"{date.Hour}{date.Minute}{date.Second}",
                payerId = ClaimUtility.GetUserId(User).Value,
                additionalData = "پس از نیم ساعت امکان لغو درخواست وجود ندارد"
            };

            var data = new
            {
                request.amount,
                request.callBackUrl,
                request.terminalId,
                request.orderId,
                request.userName,
                request.userPassword,
                request.localDate,
                request.localTime,
                request.payerId,
                request.additionalData
            };

            var ipgUrl = "https://bpm.shaparak.ir/pgwchannel/services/pgw?wsdl";
            var res = CallApi<object>(ipgUrl, data);
            res.Wait();


            return new RedirectResult("/");
        }

        [Route("/api/Payment/Verify")]
        public IActionResult Verify(string refId, long saleOrderId)
        {
            return new JsonResult('s');
        }

        public static async Task<T> CallApi<T>(string apiUrl, object value)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.SystemDefault;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                var w = client.PostAsJsonAsync(apiUrl,value);
                w.Wait();
                HttpResponseMessage response = w.Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsAsync<T>();
                    result.Wait();
                    return result.Result;
                }
                return default(T);
            }
        }
    }
}
//(DateTime.Now.Date.ToString(CultureInfo.InvariantCulture).Replace("/" , "")).Substring(0,8);
//var ipgUrl = "https://bpm.shaparak.ir/pgwchannel/startpay.mellat";