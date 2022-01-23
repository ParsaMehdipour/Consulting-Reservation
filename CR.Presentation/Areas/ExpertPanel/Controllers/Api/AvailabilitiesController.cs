using System.Collections.Generic;
using CR.Core.DTOs.Timings;
using CR.Core.Services.Interfaces.ExpertAvailabilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public List<TimingDto> GetDayDetails(long id)
        {
            return _getDayDetailsService.Execute(id).Data;
        }
    }
}
