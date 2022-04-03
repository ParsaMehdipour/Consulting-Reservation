using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Core.DTOs.ExpertAvailabilities;
using CR.Core.DTOs.RequestDTOs.Expert;
using CR.Core.DTOs.RequestDTOs.TimeOfDay;
using CR.Core.DTOs.RequestDTOs.Timing;
using CR.Core.DTOs.ResultDTOs.ExpertAvailabilities;
using CR.Core.Services.Interfaces.ExpertAvailabilities;
using CR.Core.Services.Interfaces.Experts;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CR.Presentation.Controllers.Api
{
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IGetExpertAvailabilitiesForReservationService _getExpertAvailabilitiesForReservationService;
        private readonly IGetExpertCallUsesService _getExpertCallUsesService;
        private readonly IGetTimeOfDayPriceForReservationService _getTimeOfDayPriceForReservationService;

        public ReservationController(IGetExpertAvailabilitiesForReservationService getExpertAvailabilitiesForReservationService
        , IGetExpertCallUsesService getExpertCallUsesService
        , IGetTimeOfDayPriceForReservationService getTimeOfDayPriceForReservationService)
        {
            _getExpertAvailabilitiesForReservationService = getExpertAvailabilitiesForReservationService;
            _getExpertCallUsesService = getExpertCallUsesService;
            _getTimeOfDayPriceForReservationService = getTimeOfDayPriceForReservationService;
        }

        [Route("/api/Reservation/GetDetails")]
        [HttpPost]
        public ResultDto<ResultGetExpertAvailabilitiesDetailsDto> GetDetails(RequestGetSpecificTiming model)
        {
            var date = model.date_String.ToGeorgianDateTime().AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute);

            var outPut = _getExpertAvailabilitiesForReservationService.Execute(model.expertInformationId, model.timingType, date);

            return outPut;
        }

        [Route("/api/Reservation/GetExpertCallUses")]
        [HttpPost]
        public ResultDto<ExpertCallUsesDto> GetExpertCallUses(RequestGetExpertCallUses model)
        {
            var outPut = _getExpertCallUsesService.Execute(model.expertInformationId);

            return outPut;
        }

        [Route("/api/Reservation/GetTimeOfDayPrice")]
        [HttpPost]
        public ResultDto<long> GetTimeOfDayPrice(RequestGetTimeOfDayPrice model)
        {
            var outPut = _getTimeOfDayPriceForReservationService.Execute(model.callingType, model.timeOfDayId);

            return outPut;
        }

    }
}
