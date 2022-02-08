using CR.Core.Services.Interfaces.Factors;
using Microsoft.AspNetCore.Mvc;
using ServiceReference2;
using System;
using System.Threading.Tasks;

namespace CR.Presentation.Controllers.View
{
    public class PaymentController : Controller
    {
        private readonly IGetFactorDetailsService _getFactorDetailsService;
        private readonly IUpdateFactorSaleReferenceIdService _updateFactorSaleReferenceIdService;

        public PaymentController(IGetFactorDetailsService getFactorDetailsService
        , IUpdateFactorSaleReferenceIdService updateFactorSaleReferenceIdService)
        {
            _getFactorDetailsService = getFactorDetailsService;
            _updateFactorSaleReferenceIdService = updateFactorSaleReferenceIdService;
        }

        public IActionResult Index(string factorNumber)
        {
            var model = _getFactorDetailsService.Execute(factorNumber).Data;

            ViewData["factorNumber"] = factorNumber;
            ViewData["price"] = model.price;

            return View(model);
        }


        [HttpPost]
        public IActionResult Verify(string RefId, string ResCode, long saleOrderId, long SaleReferenceId)
        {
            var factor = _getFactorDetailsService.Execute(saleOrderId.ToString()).Data;

            if (factor == null)
            {
                ViewData["Description"] = "فاکتور معتبر نمی باشد!!";
                return View();
            }
            else if (factor.refId != RefId)
            {
                ViewData["Description"] = "تراکنش معتبر نمی باشد";
                return View();
            }

            _updateFactorSaleReferenceIdService.Execute(saleOrderId.ToString(), SaleReferenceId);

            var res = CallApi(saleOrderId.ToString(), SaleReferenceId);

            res.Wait();

            var resCode = res.Result.Body.@return;

            if (resCode == "0")
            {
                ViewData["Description"] = "تراکنش با موفقیت انجام شد";

                return View();
            }

            ViewData["Description"] = "پرداخت نا موفق";

            return View();

        }

        private async Task<bpVerifyRequestResponse> CallApi(string factorNumber, long saleReferenceId)
        {
            try
            {
                var date = DateTime.Now;

                PaymentGatewayClient client = new PaymentGatewayClient(PaymentGatewayClient.EndpointConfiguration.PaymentGatewayImplPort);

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
