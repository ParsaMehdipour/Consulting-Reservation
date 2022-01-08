using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Consumers;
using CR.Core.Services.Interfaces.Consumers;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.ConsumerPanel.Controllers.Api
{
    [ApiController]
    public class ConsumerDetailsController : ControllerBase
    {
        private readonly IGetConsumerDetailsForPartialService _getConsumerDetailsForPartialService;

        public ConsumerDetailsController(IGetConsumerDetailsForPartialService getConsumerDetailsForPartialService)
        {
            _getConsumerDetailsForPartialService = getConsumerDetailsForPartialService;
        }

        [Route("/api/ConsumerDetails/GetDetails")]
        [HttpGet]
        public ResultDto<ConsumerForPartialDto> GetDetails()
        {
            var consumerId = ClaimUtility.GetUserId(User).Value;

            return _getConsumerDetailsForPartialService.Execute(consumerId);
        }
    }
}
