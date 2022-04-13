using CR.Common.Utilities;
using CR.Core.DTOs.RequestDTOs.FinancialTransactions;
using CR.Core.DTOs.SMS;
using CR.Core.Services.Interfaces.FinancialTransaction;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CR.Presentation.Areas.ExpertPanel.Controllers.Api
{
    [ApiController]
    public class FinancialTransactionsController : ControllerBase
    {
        private readonly IAddCheckoutFinancialTransactionService _addCheckoutFinancialTransactionService;
        private readonly IGetCheckoutFinancialTransactionDescriptionService _getCheckoutFinancialTransactionDescriptionService;
        private readonly IAddDeclineAppointmentExpertSideFinancialTransactionService _addDeclineAppointmentExpertSideFinancialTransactionService;

        public FinancialTransactionsController(IAddCheckoutFinancialTransactionService addCheckoutFinancialTransactionService
        , IGetCheckoutFinancialTransactionDescriptionService getCheckoutFinancialTransactionDescriptionService
        , IAddDeclineAppointmentExpertSideFinancialTransactionService addDeclineAppointmentExpertSideFinancialTransactionService)
        {
            _addCheckoutFinancialTransactionService = addCheckoutFinancialTransactionService;
            _getCheckoutFinancialTransactionDescriptionService = getCheckoutFinancialTransactionDescriptionService;
            _addDeclineAppointmentExpertSideFinancialTransactionService = addDeclineAppointmentExpertSideFinancialTransactionService;
        }

        [Route("/api/FinancialTransactions/RequestCheckout")]
        [HttpPost]
        public IActionResult RequestCheckout([FromForm] RequestCheckoutDto model)
        {
            var receiverId = ClaimUtility.GetUserId(User).Value;

            var result = _addCheckoutFinancialTransactionService.Execute(receiverId, model.price);

            return new JsonResult(result);
        }

        [Route("/api/CheckoutFinancialTransactions/GetDescriptionForExpert")]
        [HttpPost]
        public IActionResult GetDescriptionForExpert([FromBody] RequestGetCheckoutFinancialTransactionDescriptionDto model)
        {
            var result = _getCheckoutFinancialTransactionDescriptionService.Execute(model.id).Data;

            return new JsonResult(result);
        }

        [Route("/api/CheckoutFinancialTransactions/DeclineAppointmentExpertSide")]
        [HttpPost]
        public async Task<IActionResult> DeclineAppointmentExpertSide(RequestAddDeclineAppointmentTransactionExpertSideDto model)
        {
            var result = _addDeclineAppointmentExpertSideFinancialTransactionService.Execute(model.receiverId, model.appointmentId);

            if (result.IsSuccess)
            {
                var modelExpert = new SMSModel()
                {
                    toNum = result.Data.phoneNumber,
                    patternCode = SMSPatterns.ReservationDeclined_ExpertSide,
                    inputData = new List<Dictionary<string, string>>()
                    {
                        new Dictionary<string, string>
                        {
                            {SMSInputs.Date, result.Data.date},
                            {SMSInputs.Time, result.Data.time}
                        },
                    },
                };

                var uri = "https://ippanel.com/api/select";

                await CallApiReservation<object>(uri, modelExpert);
            }

            return new JsonResult(result);
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
