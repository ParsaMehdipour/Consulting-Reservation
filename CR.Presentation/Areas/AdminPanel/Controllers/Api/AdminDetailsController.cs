using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Users;
using CR.Core.Services.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers.Api
{
    [ApiController]
    public class AdminDetailsController : ControllerBase
    {
        private readonly IGetAdminDetailsForPartialService _getAdminDetailsForPartialService;

        public AdminDetailsController(IGetAdminDetailsForPartialService getAdminDetailsForPartialService)
        {
            _getAdminDetailsForPartialService = getAdminDetailsForPartialService;
        }

        [Route("/api/AdminDetails/GetDetails")]
        [HttpGet]
        public ResultDto<AdminForPartialDto> GetDetails()
        {
            var userId = ClaimUtility.GetUserId(User).Value;

            return _getAdminDetailsForPartialService.Execute(userId);
        }
    }
}
