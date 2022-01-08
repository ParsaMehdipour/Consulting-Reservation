using CR.Common.Utilities;
using CR.Core.Services.Interfaces.Users;
using CR.DataAccess.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CR.Presentation.Controllers.Api
{
    [ApiController]
    [Authorize]
    public class SharedController : ControllerBase
    {
        private readonly IGetUserFlagService _getUserFlagService;

        public SharedController(IGetUserFlagService getUserFlagService)
        {
            _getUserFlagService = getUserFlagService;
        }

        [Route("/api/Shared/GetUserFlag")]
        [HttpGet]
        public UserFlag GetUserFlag()
        {
            var userId = ClaimUtility.GetUserId(User).Value;

            return _getUserFlagService.Execute(userId).UserFlag;
        }
    }
}
