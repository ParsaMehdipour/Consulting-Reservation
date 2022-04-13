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

namespace CR.Presentation.Areas.AdminPanel.Controllers.Api
{
    [ApiController]
    public class FinancialTransactionsController : ControllerBase
    {
        private readonly IAddDeclineFactorFinancialTransactionService _addDeclineFactorFinancialTransactionService;
        private readonly IAddDeclineAppointmentFinancialTransactionService _addDeclineAppointmentFinancialTransactionService;

        public FinancialTransactionsController(IAddDeclineFactorFinancialTransactionService addDeclineFactorFinancialTransactionService
        , IAddDeclineAppointmentFinancialTransactionService addDeclineAppointmentFinancialTransactionService)
        {
            _addDeclineFactorFinancialTransactionService = addDeclineFactorFinancialTransactionService;
            _addDeclineAppointmentFinancialTransactionService = addDeclineAppointmentFinancialTransactionService;
        }

        [Route("/api/FinancialTransactions/DeclineFactor")]
        [HttpPost]
        public IActionResult DeclineFactor(RequestDeclineFinancialTransactionDto request)
        {
            var result = _addDeclineFactorFinancialTransactionService.Execute(request.payerId, request.factorId);

            return new JsonResult(result);
        }

        [Route("/api/FinancialTransactions/DeclineAppointment")]
        [HttpPost]
        public async Task<IActionResult> DeclineAppointment(RequestAddDeclineAppointmentTransactionAdminSideDto request)
        {
            var result = _addDeclineAppointmentFinancialTransactionService.Execute(request.receiverId, request.appointmentId);

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
