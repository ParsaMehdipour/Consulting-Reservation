using CR.Common.ActiveMenus;
using CR.Core.DTOs.RequestDTOs;
using CR.Core.DTOs.Users;
using CR.Core.Services.Interfaces.Consumers;
using CR.Core.Services.Interfaces.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CR.Presentation.Areas.AdminPanel.Controllers
{
    [Authorize]
    [Area("AdminPanel")]
    public class ConsumersController : Controller
    {
        private readonly IGetAllConsumersService _getAllConsumersService;
        private readonly IRegisterConsumerFromAdminService _registerConsumerFromAdminService;
        private readonly IGetConsumerDetailsForProfileService _getConsumerDetailsForProfileService;
        private readonly IEditConsumerDetailsService _editConsumerDetailsService;
        private readonly IAddConsumerDetailsService _addConsumerDetailsService;

        public ConsumersController(IGetAllConsumersService getAllConsumersService
        , IRegisterConsumerFromAdminService registerConsumerFromAdminService
        , IGetConsumerDetailsForProfileService getConsumerDetailsForProfileService
        , IEditConsumerDetailsService editConsumerDetailsService
        , IAddConsumerDetailsService addConsumerDetailsService)
        {
            _getAllConsumersService = getAllConsumersService;
            _registerConsumerFromAdminService = registerConsumerFromAdminService;
            _getConsumerDetailsForProfileService = getConsumerDetailsForProfileService;
            _editConsumerDetailsService = editConsumerDetailsService;
            _addConsumerDetailsService = addConsumerDetailsService;
        }

        public IActionResult Index(int page = 1, int pageSize = 20)
        {
            TempData["activemenu"] = ActiveMenu.Consumers;

            var model = _getAllConsumersService.Execute(page, pageSize).Data;

            return View(model);
        }

        [HttpGet]
        public IActionResult AddNewConsumer()
        {
            TempData["activemenu"] = ActiveMenu.Consumers;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewConsumer(RegisterConsumerFromAdminDto request)
        {
            var result = await _registerConsumerFromAdminService.Execute(request);

            return new JsonResult(result);
        }

        [HttpGet]
        public IActionResult ConsumerDetails(long id)
        {
            TempData["activemenu"] = ActiveMenu.Consumers;

            ViewData["id"] = id;

            var model = _getConsumerDetailsForProfileService.Execute(id).Data.ConsumerDetailsForSiteDto;

            return View(model);
        }

        [HttpPost]
        public IActionResult EditConsumerDetails(RequestEditConsumerDetailsDto request)
        {
            var result = _editConsumerDetailsService.Execute(request);

            return new JsonResult(result);
        }

        [HttpPost]
        public IActionResult AddProfileDetails(RequestAddConsumerDetailsDto request)
        {
            var result = _addConsumerDetailsService.Execute(request);

            return new JsonResult(result);
        }
    }
}
