using CR.Common.DTOs;
using CR.Core.Services.Interfaces.Factors;
using CR.DataAccess.Enums;
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
        private readonly IUpdateFactorCartHolderPanService _updateFactorCartHolderPanService;
        private readonly IUpdateFactorStatusService _updateFactorStatusService;

        public PaymentController(IGetFactorDetailsService getFactorDetailsService
        , IUpdateFactorSaleReferenceIdService updateFactorSaleReferenceIdService
        , IUpdateFactorCartHolderPanService updateFactorCartHolderPanService
        , IUpdateFactorStatusService updateFactorStatusService)
        {
            _getFactorDetailsService = getFactorDetailsService;
            _updateFactorSaleReferenceIdService = updateFactorSaleReferenceIdService;
            _updateFactorCartHolderPanService = updateFactorCartHolderPanService;
            _updateFactorStatusService = updateFactorStatusService;
        }

        public IActionResult Index(string factorNumber)
        {
            var model = _getFactorDetailsService.Execute(factorNumber).Data;

            ViewData["factorNumber"] = factorNumber;
            ViewData["price"] = model.price;

            return View(model);
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
                var factor = _getFactorDetailsService.Execute(SaleOrderId.ToString()).Data;

                if (factor == null)
                {
                    ViewData["Description"] = "فاکتور معتبر نمی باشد!!";
                    return View(result);
                }

                if (factor.refId != RefId)
                {
                    ViewData["Description"] = "تراکنش معتبر نمی باشد";
                    return View(result);
                }

                if (factor.price != FinalAmount / 10)
                {
                    ViewData["Description"] = "تراکنش معتبر نمی باشد";
                    return View(result);
                }

                _updateFactorSaleReferenceIdService.Execute(SaleOrderId.ToString(), SaleReferenceId);

                //var res = CallApi(SaleOrderId.ToString(), SaleReferenceId);

                //res.Wait();

                var resCode = "0";

                if (resCode == "0")
                {
                    _updateFactorCartHolderPanService.Execute(SaleOrderId.ToString(), CardHolderPan);

                    _updateFactorStatusService.Execute(SaleOrderId.ToString(), FactorStatus.SuccessfulPayment, TransactionStatus.Successful);

                    ViewData["Description"] = "تراکنش با موفقیت انجام شد، کد رهگیری پرداخت شما : " + SaleReferenceId;

                    var temp = ViewData["Description"];

                    result.IsSuccess = true;

                    return View(result);
                }

                ViewData["Description"] = "تراکنش ناموفق";

                ViewData["ResCode"] = resCode;

                _updateFactorStatusService.Execute(SaleOrderId.ToString(), FactorStatus.UnsuccessfulPayment, TransactionStatus.Failed);

                return View(result);
            }

            ViewData["Description"] = "پرداخت ناموفق";

            _updateFactorStatusService.Execute(SaleOrderId.ToString(), FactorStatus.UnsuccessfulPayment, TransactionStatus.Failed);

            ViewData["ResCode"] = ResCode;

            return View(result);
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
