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

namespace CR.Presentation.Areas.ConsumerPanel.Controllers.Api
{
    [ApiController]
    public class FinancialTransactionsController : ControllerBase
    {
        private readonly IAddDeclineAppointmentConsumerSideFinancialTransactionService _addDeclineAppointmentConsumerSideFinancialTransactionService;

        public FinancialTransactionsController(IAddDeclineAppointmentConsumerSideFinancialTransactionService addDeclineAppointmentConsumerSideFinancialTransactionService)
        {
            _addDeclineAppointmentConsumerSideFinancialTransactionService = addDeclineAppointmentConsumerSideFinancialTransactionService;
        }

        [Route("/api/FinancialTransactions/DeclineAppointmentConsumerSide")]
        [HttpPost]
        public async Task<IActionResult> DeclineAppointmentConsumerSide(RequestAddDeclineAppointmentTransactionConsumerSideDto model)
        {
            var userId = ClaimUtility.GetUserId(User).Value;

            var result = _addDeclineAppointmentConsumerSideFinancialTransactionService.Execute(userId, model.appointmentId);

            if (result.IsSuccess)
            {
                var modelExpert = new SMSModel()
                {
                    toNum = result.Data.phoneNumber,
                    patternCode = SMSPatterns.ReservationDeclined_ConsumerSide,
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
