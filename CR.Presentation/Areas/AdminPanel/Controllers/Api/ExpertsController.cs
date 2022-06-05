using CR.Core.DTOs.RequestDTOs;
using CR.Core.Services.Interfaces.Specialites;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers.Api
{
    [ApiController]
    public class ExpertsController : ControllerBase
    {
        private readonly IGetAllSpecialitiesService _getAllSpecialitiesService;

        public ExpertsController(IGetAllSpecialitiesService getAllSpecialitiesService)
        {
            _getAllSpecialitiesService = getAllSpecialitiesService;
        }

        [HttpGet]
        [Route("/api/Experts/select2")]
        public IActionResult Select2(string search, int page, int specialtyId)
        {
            Select2Request model = new Select2Request { search = search, page = page, parentId = specialtyId };

            var results = _getAllSpecialitiesService.GetAllAttributeTypeForSelect2(model);

            return new JsonResult(results);
        }

        [HttpGet]
        [Route("/api/Experts/getselect2items")]
        public IActionResult GetSelect2ItemsForExpert(long expertid)
        {
            var result = _getAllSpecialitiesService.GetSelect2ItemsForExpert(expertid);

            return new JsonResult(result);
        }
    }

}
