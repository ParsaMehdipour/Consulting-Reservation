using CR.Common.Utilities;
using CR.Core.DTOs.Timings;
using CR.Core.Services.Interfaces.ExpertAvailabilities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CR.Presentation.Areas.ExpertPanel.Controllers.Api
{
    [ApiController]
    public class AvailabilitiesController : ControllerBase
    {
        private readonly IGetDayDetailsService _getDayDetailsService;

        public AvailabilitiesController(IGetDayDetailsService getDayDetailsService)
        {
            _getDayDetailsService = getDayDetailsService;
        }

        [Route("api/Availabilities/GetDayDetails")]
        [HttpGet]
        public List<TimingForEditDto> GetDayDetails(long id)
        {
            var expertId = ClaimUtility.GetUserId(User).Value;

            return _getDayDetailsService.Execute(id, expertId).Data;
        }
    }
}
