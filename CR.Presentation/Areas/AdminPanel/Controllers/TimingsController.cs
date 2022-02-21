using CR.Common.ActiveMenus;
using CR.Core.DTOs.RequestDTOs;
using CR.Core.Services.Interfaces.Timings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize]
    public class TimingsController : Controller
    {
        private readonly IGetAllTimingsForAdminService _getAllTimingsForAdminService;
        private readonly IAddNewTimingService _addNewTimingService;
        private readonly IRemoveTimingService _removeTimingService;

        public TimingsController(IGetAllTimingsForAdminService getAllTimingsForAdminService
        , IAddNewTimingService addNewTimingService
        , IRemoveTimingService removeTimingService)
        {
            _getAllTimingsForAdminService = getAllTimingsForAdminService;
            _addNewTimingService = addNewTimingService;
            _removeTimingService = removeTimingService;
        }

        public IActionResult Index(int page = 1, int pageSize = 20)
        {
            TempData["activemenu"] = ActiveMenu.Timings;


            var model = _getAllTimingsForAdminService.Execute(page, pageSize).Data;

            return View(model);
        }

        [HttpPost]
        public IActionResult AddTimings(RequestAddNewTimingDto request)
        {
            var result = _addNewTimingService.Execute(request);

            return new JsonResult(result);
        }

        [HttpPost]
        public IActionResult Remove(long id)
        {
            var result = _removeTimingService.Execute(id);

            return new JsonResult(result);
        }
    }
}
