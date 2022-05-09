using CR.Common.Utilities;
using CR.Core.DTOs.SMS;
using CR.Core.Services.Interfaces.Appointment;
using CR.Core.Services.Interfaces.FinancialTransaction;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CR.Core.Services.Implementations.Appointment
{
    public class ResetAppointmentStatusService : IResetAppointmentStatusService
    {
        private readonly ApplicationContext _context;
        private readonly IAddDeclineAppointmentFinancialTransactionService _addDeclineAppointmentFinancialTransactionService;

        public ResetAppointmentStatusService(ApplicationContext context
        , IAddDeclineAppointmentFinancialTransactionService addDeclineAppointmentFinancialTransactionService)
        {
            _context = context;
            _addDeclineAppointmentFinancialTransactionService = addDeclineAppointmentFinancialTransactionService;
        }

        public void Execute()
        {
            var appoinments = _context.Appointments
                .Include(_ => _.ConsumerInformation)
                .Include(_ => _.TimeOfDay)
                .Include(_ => _.ChatUsers)
                .ThenInclude(_ => _.ChatUserMessages)
                .Where(_ => _.TimeOfDay.StartTime.AddMinutes(5) <= DateTime.Now
                            && _.ChatUsers.Count > 0
                            && _.IsClosed == false
                            && !(_.ChatUsers.FirstOrDefault().ChatUserMessages.Any(cm =>
                                cm.MessageFlag == MessageFlag.ExpertMessage
                                && cm.CreateDate <= _.TimeOfDay.StartTime.AddMinutes(5)))).ToList();

            foreach (var appointment in appoinments)
            {
                appointment.IsClosed = true;
                appointment.ChatUsers.FirstOrDefault()!.ChatStatus = ChatStatus.Closed;

                _context.SaveChanges();

                var result = _addDeclineAppointmentFinancialTransactionService.Execute(appointment.ConsumerInformation.ConsumerId, appointment.Id);

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

                    CallApiReservation<object>(uri, modelExpert);
                }

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
