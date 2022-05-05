using CR.Common.DTOs;
using CR.Core.DTOs.Images;
using CR.Core.DTOs.RequestDTOs.Chat;
using CR.Core.Services.Interfaces.ChatMessages;
using CR.Core.Services.Interfaces.ChatUsers;
using CR.Core.Services.Interfaces.Images;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.ExpertPanel.Controllers.Api
{
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IGetChatMessagesService _getChatMessagesService;
        private readonly IAddNewChatMessageService _addNewChatMessageService;
        private readonly IAddNewVoiceMessageService _addNewVoiceMessageService;
        private readonly IImageUploaderService _imageUploaderService;
        private readonly ICheckForAppointmentTimeService _checkForAppointmentTimeService;

        public ChatController(IGetChatMessagesService getChatMessagesService
        , IAddNewChatMessageService addNewChatMessageService
        , IAddNewVoiceMessageService addNewVoiceMessageService
        , IImageUploaderService imageUploaderService)
        {
            _getChatMessagesService = getChatMessagesService;
            _addNewChatMessageService = addNewChatMessageService;
            _addNewVoiceMessageService = addNewVoiceMessageService;
            _imageUploaderService = imageUploaderService;
        }

        [Route("/api/Chat/GetMessages")]
        [HttpPost]
        public IActionResult GetMessages([FromBody] RequestGetChatMessagesDto request)
        {
            var result = _getChatMessagesService.Execute(request.chatUserId, true);

            return new JsonResult(result);
        }

        [Route("/api/Chat/UploadImage")]
        [HttpPost]
        public IActionResult UploadImage([FromForm] RequestAddNewMessageWithImageDto request)
        {
            var result = new ResultDto<string>()
            {
                IsSuccess = true,
            };

            if (request.file != null)
            {
                var filePath = _imageUploaderService.Execute(new UploadImageDto()
                {
                    File = request.file,
                    Folder = "Chat"
                });

                result.Data = filePath;
            }

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
