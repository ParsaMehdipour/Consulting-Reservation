using CR.Common.Utilities;
using CR.Core.Services.Interfaces.ChatUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.ConsumerPanel.Controllers.View
{
    [Area("ConsumerPanel")]
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IGetConsumerChatUsersService _getConsumerChatUsersService;

        public ChatController(IGetConsumerChatUsersService getConsumerChatUsersService)
        {
            _getConsumerChatUsersService = getConsumerChatUsersService;
        }

        public IActionResult Index(string searchKey)
        {
            var consumerId = ClaimUtility.GetUserId(User).Value;

            var model = _getConsumerChatUsersService.Execute(consumerId, searchKey).Data;

            return View(model);
        }

        public IActionResult VoiceRecord()
        {
            return View();
        }
    }
}
