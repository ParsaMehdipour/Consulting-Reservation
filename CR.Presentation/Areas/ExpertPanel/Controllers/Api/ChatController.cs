using CR.Core.DTOs.RequestDTOs.Chat;
using CR.Core.Services.Interfaces.ChatMessages;
using CR.DataAccess.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.ExpertPanel.Controllers.Api
{
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IGetChatMessagesService _getChatMessagesService;
        private readonly IAddNewChatMessageService _addNewChatMessageService;
        private readonly IAddNewVoiceMessageService _addNewVoiceMessageService;

        public ChatController(IGetChatMessagesService getChatMessagesService
        , IAddNewChatMessageService addNewChatMessageService
        , IAddNewVoiceMessageService addNewVoiceMessageService)
        {
            _getChatMessagesService = getChatMessagesService;
            _addNewChatMessageService = addNewChatMessageService;
            _addNewVoiceMessageService = addNewVoiceMessageService;
        }

        [Route("/api/Chat/GetMessages")]
        [HttpPost]
        public IActionResult GetMessages([FromBody] RequestGetChatMessagesDto request)
        {
            var result = _getChatMessagesService.Execute(request.chatUserId, true);

            return new JsonResult(result);
        }

        [Route("/api/Chat/AddNewMessage")]
        [HttpPost]
        public IActionResult AddNewMessage([FromForm] RequestAddNewChatMessageDto request)
        {
            request.messageFlag = MessageFlag.ExpertMessage;

            var result = _addNewChatMessageService.Execute(request);

            return new JsonResult(result);
        }

        [Route("/api/Chat/AddNewVoiceMessage")]
        [HttpPost]
        public IActionResult AddNewVoiceMessage([FromForm] RequestAddNewVoiceMessageDto request)
        {
            var result = _addNewVoiceMessageService.Execute(request);

            return new JsonResult(result);
        }
    }
}
