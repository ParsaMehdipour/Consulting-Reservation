using CR.Common.DTOs;
using CR.Core.DTOs.ExpertAvailabilities;
using CR.Core.DTOs.RequestDTOs.Timing;
using CR.Core.Services.Interfaces.ExpertAvailabilities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CR.Presentation.Controllers.Api
{
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IGetExpertAvailabilitiesForReservationService _getExpertAvailabilitiesForReservationService;

        public ReservationController(IGetExpertAvailabilitiesForReservationService getExpertAvailabilitiesForReservationService)
        {
            _getExpertAvailabilitiesForReservationService = getExpertAvailabilitiesForReservationService;
        }

        [Route("/api/Reservation/GetDetails")]
        [HttpPost]
        public ResultDto<List<DayDto>> GetDetails(RequestGetSpecificTiming model)
        {
            var outPut = _getExpertAvailabilitiesForReservationService.Execute(model.expertInformationId, model.timingType);

            return outPut;
        }

    }
}
