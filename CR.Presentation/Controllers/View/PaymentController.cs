using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.RequestDTOs.ChatUser;
using CR.Core.DTOs.SMS;
using CR.Core.Services.Interfaces.ChatUsers;
using CR.Core.Services.Interfaces.Factors;
using CR.Core.Services.Interfaces.FinancialTransaction;
using CR.DataAccess.Enums;
using Microsoft.AspNetCore.Mvc;
using ServiceReference2;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CR.Presentation.Controllers.View
{
    public class PaymentController : Controller
    {
        private readonly IGetFactorDetailsService _getFactorDetailsService;
        private readonly IUpdateFactorSaleReferenceIdService _updateFactorSaleReferenceIdService;
        private readonly IUpdateFactorCartHolderPanService _updateFactorCartHolderPanService;
        private readonly IUpdateFactorStatusService _updateFactorStatusService;
        private readonly IGetFinancialTransactionDetailsForVerifyService _getFinancialTransactionDetailsForVerifyService;
        private readonly IAddNewChatUserService _addNewChatUserService;

        public PaymentController(IGetFactorDetailsService getFactorDetailsService
        , IUpdateFactorSaleReferenceIdService updateFactorSaleReferenceIdService
        , IUpdateFactorCartHolderPanService updateFactorCartHolderPanService
        , IUpdateFactorStatusService updateFactorStatusService
        , IGetFinancialTransactionDetailsForVerifyService getFinancialTransactionDetailsForVerifyService
        , IAddNewChatUserService addNewChatUserService)
        {
            _getFactorDetailsService = getFactorDetailsService;
            _updateFactorSaleReferenceIdService = updateFactorSaleReferenceIdService;
            _updateFactorCartHolderPanService = updateFactorCartHolderPanService;
            _updateFactorStatusService = updateFactorStatusService;
            _getFinancialTransactionDetailsForVerifyService = getFinancialTransactionDetailsForVerifyService;
            _addNewChatUserService = addNewChatUserService;
        }

        public IActionResult Index(string factorNumber)
        {
            var model = _getFactorDetailsService.Execute(factorNumber).Data;

            ViewData["factorId"] = model.Id;
            ViewData["price"] = model.price;

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Verify(string RefId, string ResCode, long SaleOrderId, long SaleReferenceId, string CardHolderPan, long FinalAmount)
        {
            var result = new ResultDto()
            {
                IsSuccess = false
            };

            var factor = _getFinancialTransactionDetailsForVerifyService.Execute(SaleOrderId.ToString()).Data;

            if (ResCode == "0")
            {

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

                _updateFactorSaleReferenceIdService.Execute(factor.Id, SaleReferenceId);

                var res = CallApi(SaleOrderId.ToString(), SaleReferenceId);

                res.Wait();

                var resCode = res.Result.Body.@return;

                if (resCode == "0")
                {
                    _updateFactorCartHolderPanService.Execute(factor.Id, CardHolderPan);

                    var updateStatusResult = _updateFactorStatusService.Execute(factor.Id, FactorStatus.SuccessfulPayment, TransactionStatus.Successful);

                    if (updateStatusResult.IsSuccess)
                    {
                        if (updateStatusResult.Data.IsChat)
                        {
                            foreach (var chatAppointment in updateStatusResult.Data.chatAppointments)
                            {
                                _addNewChatUserService.Execute(new RequestAddNewChatUserDto()
                                {
                                    consumerId = updateStatusResult.Data.ConsumerId,
                                    expertInformationId = updateStatusResult.Data.ExpertInformationId,
                                    messageType = chatAppointment.CallingType,
                                    appointmentDate = chatAppointment.AppointmentDate
                                });
                            }
                        }

                        foreach (var appointment in updateStatusResult.Data.AppointmentDetailsForConsumerSmsDtos)
                        {
                            var model = new SMSModel()
                            {
                                toNum = updateStatusResult.Data.ConsumerPhoneNum,
                                patternCode = SMSPatterns.ReservationSuccessfulPatternCode_ConsumerSide,
                                inputData = new List<Dictionary<string, string>>()
                                {
                                    new Dictionary<string, string>
                                    {
                                        {SMSInputs.UserName, appointment.UserName },
                                        {SMSInputs.Date, appointment.Date},
                                        {SMSInputs.Time, appointment.Time}
                                    },
                                },
                            };

                            var uri = "https://ippanel.com/api/select";

                            await CallApiReservation<object>(uri, model);
                        }

                        foreach (var appointment in updateStatusResult.Data.AppointmentDetailsForExpertSmsDtos)
                        {
                            var modelExpert = new SMSModel()
                            {
                                toNum = updateStatusResult.Data.ExpertPhoneNum,
                                patternCode = SMSPatterns.ReservationSuccessfulPatternCode_Expert_Side,
                                inputData = new List<Dictionary<string, string>>()
                                {
                                    new Dictionary<string, string>
                                    {
                                        {SMSInputs.UserName, appointment.UserName },
                                        {SMSInputs.Date, appointment.Date},
                                        {SMSInputs.Time, appointment.Time}
                                    },
                                },
                            };

                            var uri = "https://ippanel.com/api/select";

                            await CallApiReservation<object>(uri, modelExpert);
                        }
                    }

                    if (updateStatusResult.Data.IsChat)
                    {
                        ViewData["Description"] = "تراکنش با موفقیت انجام شد ، مشاور به لیست کاربران برای ارسال پیام شما اضافه شد ، کد رهگیری پرداخت شما : " + SaleReferenceId;
                    }
                    else
                    {
                        ViewData["Description"] = "تراکنش با موفقیت انجام شد، کد رهگیری پرداخت شما : " + SaleReferenceId;
                    }

                    var temp = ViewData["Description"];

                    result.IsSuccess = true;

                    return View(result);
                }

                ViewData["Description"] = "تراکنش ناموفق";

                ViewData["ResCode"] = resCode;

                _updateFactorStatusService.Execute(factor.Id, FactorStatus.UnsuccessfulPayment, TransactionStatus.Failed);

                return View(result);
            }

            ViewData["Description"] = "پرداخت ناموفق";

            _updateFactorStatusService.Execute(factor.Id, FactorStatus.UnsuccessfulPayment, TransactionStatus.Failed);

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

        private async Task<T> CallApiReservation<T>(string apiUrl, object value)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.SystemDefault;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                var w = client.PostAsJsonAsync(apiUrl, value);
                w.Wait();
                HttpResponseMessage response = w.Result;
                if (response.IsSuccessStatusCode)
                {

                    return default(T);
                }
                return default(T);
            }
        }
    }
}
