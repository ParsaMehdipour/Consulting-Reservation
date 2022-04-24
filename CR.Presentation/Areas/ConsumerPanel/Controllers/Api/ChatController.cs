using CR.Core.DTOs.RequestDTOs.Chat;
using CR.Core.DTOs.RequestDTOs.ChatUser;
using CR.Core.Services.Interfaces.ChatMessages;
using CR.Core.Services.Interfaces.ChatUsers;
using CR.Core.Services.Interfaces.Images;
using CR.DataAccess.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.ConsumerPanel.Controllers.Api
{
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IGetChatMessagesService _getChatMessagesService;
        private readonly IAddNewChatMessageService _addNewChatMessageService;
        private readonly IAddNewVoiceMessageService _addNewVoiceMessageService;
        private readonly ICheckForAppointmentTimeService _checkForAppointmentTimeService;
        private readonly IImageUploaderService _imageUploaderService;

        public ChatController(IGetChatMessagesService getChatMessagesService
        , IAddNewChatMessageService addNewChatMessageService
        , IAddNewVoiceMessageService addNewVoiceMessageService
        , ICheckForAppointmentTimeService checkForAppointmentTimeService
        , IImageUploaderService imageUploaderService)
        {
            _getChatMessagesService = getChatMessagesService;
            _addNewChatMessageService = addNewChatMessageService;
            _addNewVoiceMessageService = addNewVoiceMessageService;
            _checkForAppointmentTimeService = checkForAppointmentTimeService;
            _imageUploaderService = imageUploaderService;
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

        [Route("/api/Chat/AddNewConsumerVoiceMessage")]
        [HttpPost]
        public IActionResult AddNewVoiceMessage([FromForm] RequestAddNewVoiceMessageDto request)
        {
            var result = _addNewVoiceMessageService.Execute(request);

            return new JsonResult(result);
        }

        [Route("/api/Chat/CheckTime")]
        [HttpPost]
        public IActionResult CheckTime([FromForm] RequestCheckForAppointmentTimeDto request)
        {
            var result = _checkForAppointmentTimeService.Execute(request);

            return new JsonResult(result);
        }
    }
}
