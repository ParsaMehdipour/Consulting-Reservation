using CR.Core.DTOs.RequestDTOs.Chat;
using CR.Core.Services.Interfaces.ChatMessages;
using CR.DataAccess.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.ConsumerPanel.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IGetChatMessagesService _getChatMessagesService;
        private readonly IAddNewChatMessageService _addNewChatMessageService;

        public ChatController(IGetChatMessagesService getChatMessagesService
        , IAddNewChatMessageService addNewChatMessageService)
        {
            _getChatMessagesService = getChatMessagesService;
            _addNewChatMessageService = addNewChatMessageService;
        }

        [Route("/api/Chat/GetConsumerMessages")]
        [HttpPost]
        public IActionResult GetMessages([FromBody] RequestGetChatMessagesDto request)
        {
            var result = _getChatMessagesService.Execute(request.chatUserId, false);

            return new JsonResult(result);
        }

        [Route("/api/Chat/AddNewConsumerMessage")]
        [HttpPost]
        public IActionResult AddNewMessage([FromForm] RequestAddNewChatMessageDto request)
        {
            request.messageFlag = MessageFlag.ConsumerMessage;

            var result = _addNewChatMessageService.Execute(request);

            return new JsonResult(result);
        }
    }
}
