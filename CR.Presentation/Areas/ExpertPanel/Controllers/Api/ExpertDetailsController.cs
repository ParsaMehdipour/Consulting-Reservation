using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Experts;
using CR.Core.DTOs.RequestDTOs;
using CR.Core.Services.Interfaces.Experts;
using CR.Core.Services.Interfaces.Specialites;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.ExpertPanel.Controllers.Api
{
    [ApiController]
    public class ExpertDetailsController : ControllerBase
    {
        private readonly IGetExpertDetailsForPartialService _getExpertDetailsForPartialService;
        private readonly IGetAllSpecialitiesService _getAllSpecialitiesService;

        public ExpertDetailsController(IGetExpertDetailsForPartialService getExpertDetailsForPartialService, IGetAllSpecialitiesService getAllSpecialitiesService)
        {
            _getExpertDetailsForPartialService = getExpertDetailsForPartialService;
            _getAllSpecialitiesService = getAllSpecialitiesService;
        }

        [Route("/api/ExpertDetails/GetDetails")]
        [HttpGet]
        public ResultDto<ExpertForPartialDto> GetDetails()
        {
            var expertId = ClaimUtility.GetUserId(User).Value;

            return _getExpertDetailsForPartialService.Execute(expertId);
        }

        [HttpGet]
        [Route("/api/ExpertDetails/select2")]
        public IActionResult Select2(string search, int page, int specialtyId)
        {
            Select2Request model = new Select2Request { search = search, page = page, parentId = specialtyId };

            var results = _getAllSpecialitiesService.GetAllAttributeTypeForSelect2(model);

            return new JsonResult(results);
        }

        [HttpGet]
        [Route("/api/ExpertDetails/getselect2items")]
        public IActionResult GetSelect2ItemsForExpert(long expertid)
        {
            var result = _getAllSpecialitiesService.GetSelect2ItemsForExpert(expertid);

            return new JsonResult(result);
        }

    }
}
