using CR.Core.DTOs.Timings;
using CR.Core.Services.Interfaces.ExpertAvailabilities;
using CR.Core.Services.Interfaces.Timings;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CR.Presentation.Areas.AdminPanel.Controllers.Api
{
    [ApiController]
    public class AvailabilitiesController : ControllerBase
    {
        private readonly IGetDayDetailsService _getDayDetailsService;
        private readonly IGetAvailableTimingsService _getAvailableTimingsService;

        public AvailabilitiesController(IGetDayDetailsService getDayDetailsService, IGetAvailableTimingsService getAvailableTimingsService)
        {
            _getDayDetailsService = getDayDetailsService;
            _getAvailableTimingsService = getAvailableTimingsService;
        }

        [Route("/api/admin/Availabilities/GetDayDetails")]
        [HttpGet]
        public List<TimingForEditDto> GetDayDetails(long id, long expertId)
        {
            return _getDayDetailsService.Execute(id, expertId).Data;
        }

        [Route("/api/admin/Availabilities/GetAvailableTimings")]
        [HttpPost]
        public IActionResult GetAvailableTimings()
        {
            var result = _getAvailableTimingsService.Execute();

            return new JsonResult(result);
        }
    }
}
