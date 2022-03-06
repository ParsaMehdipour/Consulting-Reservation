using CR.Common.Utilities;
using CR.Core.Services.Interfaces.ChatUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.ExpertPanel.Controllers.View
{
    [Area("ExpertPanel")]
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IGetExpertChatUsersService _getExpertChatUsersService;

        public ChatController(IGetExpertChatUsersService getExpertChatUsersService)
        {
            _getExpertChatUsersService = getExpertChatUsersService;
        }

        public IActionResult Index(string searchKey)
        {
            var expertId = ClaimUtility.GetUserId(User).Value;

            var model = _getExpertChatUsersService.Execute(expertId, searchKey).Data;

            return View(model);
        }
    }
}
