using CR.Core.DTOs.RequestDTOs;
using CR.Core.Services.Interfaces.Specialites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers
{
    [Authorize]
    [Area("AdminPanel")]
    public class SpecialityController : Controller
    {
        private readonly IAddNewSpecialityService _addNewSpecialityService;
        private readonly IGetAllSpecialitiesService _getAllSpecialitiesService;
        private readonly IEditSpecialityService _editSpecialityService;
        private readonly IRemoveSpecialityService _removeSpecialityService;

        public SpecialityController(
             IAddNewSpecialityService addNewSpecialityService
            ,IGetAllSpecialitiesService getAllSpecialitiesService
            ,IEditSpecialityService editSpecialityService
            ,IRemoveSpecialityService removeSpecialityService)
        {
            _addNewSpecialityService = addNewSpecialityService;
            _getAllSpecialitiesService = getAllSpecialitiesService;
            _editSpecialityService = editSpecialityService;
            _removeSpecialityService = removeSpecialityService;
        }

        public IActionResult Index(int page = 1, int pageSize = 20)
        {
            var model = _getAllSpecialitiesService.Execute(page,pageSize).Data;

            return View(model);
        }

        [HttpPost]
        public IActionResult AddSpeciality(RequestAddNewSpecialityDto request)
        {

            if (Request.Form.Files.Count > 0)
            {
                var image = Request.Form.Files[0];
                request.File = image;
            }
            else
            {
                request.File = null;
            }
            var result = _addNewSpecialityService.Execute(request);

            return new JsonResult(result);
        }

        [HttpPost]
        public IActionResult EditSpeciality(RequestEditSpecialityDto request)
        {
            if (Request.Form.Files.Count > 0)
            {
                var image = Request.Form.Files[0];
                request.File = image;
            }
            else
            {
                request.File = null;
            }

            var result = _editSpecialityService.Execute(request);

            return new JsonResult(result);
        }

        [HttpPost]
        public IActionResult Remove(long id)
        {
            var result = _removeSpecialityService.Execute(id);

            return new JsonResult(result);
        }
    }
}
