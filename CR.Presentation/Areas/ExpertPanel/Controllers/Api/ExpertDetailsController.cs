using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Experts;
using CR.Core.Services.Interfaces.Experts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.ExpertPanel.Controllers.Api
{
    [ApiController]
    public class ExpertDetailsController : ControllerBase
    {
        private readonly IGetExpertDetailsForPartialService _getExpertDetailsForPartialService;

        public ExpertDetailsController(IGetExpertDetailsForPartialService getExpertDetailsForPartialService)
        {
            _getExpertDetailsForPartialService = getExpertDetailsForPartialService;
        }

        [Route("/api/ExpertDetails/GetDetails")]
        [HttpGet]
        public ResultDto<ExpertForPartialDto> GetDetails()
        {
            var expertId = ClaimUtility.GetUserId(User).Value;

            return _getExpertDetailsForPartialService.Execute(expertId);
        }

    }
}
