using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.RequestDTOs.ChatUser;
using CR.Core.DTOs.RequestDTOs.Wallet;
using CR.Core.DTOs.SMS;
using CR.Core.Services.Interfaces.ChatUsers;
using CR.Core.Services.Interfaces.FinancialTransaction;
using CR.Core.Services.Interfaces.Wallet;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CR.Presentation.Controllers.Api
{
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IGetWalletBalanceService _getWalletBalanceService;
        private readonly IAddPayFromWalletFinancialTransactionService _addPayFromWalletFinancialTransactionService;
        private readonly IAddNewChatUserService _addNewChatUserService;

        public WalletController(IGetWalletBalanceService getWalletBalanceService
            , IAddPayFromWalletFinancialTransactionService addPayFromWalletFinancialTransactionService
            , IAddNewChatUserService addNewChatUserService)
        {
            _getWalletBalanceService = getWalletBalanceService;
            _addPayFromWalletFinancialTransactionService = addPayFromWalletFinancialTransactionService;
            _addNewChatUserService = addNewChatUserService;
        }

        [Route("/api/Wallet/GetBalance")]
        [HttpPost]
        public ResultDto<int> GetBalance(RequestGetWalletBalanceDto request)
        {
            var balance = _getWalletBalanceService.Execute(request.payerId);

            return balance;
        }

        [Route("/api/Wallet/Pay")]
        [HttpPost]
        public async Task<IActionResult> Pay(RequestPayFromWalletDto request)
        {
            var payerId = ClaimUtility.GetUserId(User).Value;

            var result = _addPayFromWalletFinancialTransactionService.Execute(payerId, request.factorId, request.price);

            if (result.IsSuccess)
            {
                if (result.Data.IsChat)
                {
                    foreach (var chatAppointment in result.Data.chatAppointments)
                    {
                        _addNewChatUserService.Execute(new RequestAddNewChatUserDto()
                        {
                            appointmentId = chatAppointment.Id,
                            consumerId = result.Data.ConsumerId,
                            expertInformationId = result.Data.ExpertInformationId,
                            messageType = chatAppointment.CallingType,
                            appointmentDate = chatAppointment.AppointmentDate
                        });
                    }
                }

                foreach (var appointment in result.Data.AppointmentDetailsForConsumerSmsDtos)
                {
                    var model = new SMSModel()
                    {
                        toNum = result.Data.ConsumerPhoneNum,
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

                foreach (var appointment in result.Data.AppointmentDetailsForExpertSmsDtos)
                {
                    var modelExpert = new SMSModel()
                    {
                        toNum = result.Data.ExpertPhoneNum,
                        patternCode = SMSPatterns.ReservationSuccessfulPatternCode_ExpertSide,
                        inputData = new List<Dictionary<string, string>>()
                        {
                            new Dictionary<string, string>
                            {
                                {SMSInputs.Date, appointment.Date},
                                {SMSInputs.Time, appointment.Time}
                            },
                        },
                    };

                    var uri = "https://ippanel.com/api/select";

                    await CallApiReservation<object>(uri, modelExpert);
                }

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
