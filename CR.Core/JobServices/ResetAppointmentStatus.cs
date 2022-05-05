using CR.Core.Services.Interfaces.Appointment;
using Quartz;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CR.Core.JobServices
{
    public class ResetAppointmentStatus : IJob
    {
        private readonly IResetAppointmentStatusService _resetAppointmentStatusService;

        public ResetAppointmentStatus(IResetAppointmentStatusService resetAppointmentStatusService)
        {
            _resetAppointmentStatusService = resetAppointmentStatusService;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _resetAppointmentStatusService.Execute();

            return Task.CompletedTask;
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
