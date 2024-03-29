﻿using CR.Common.DTOs;
using CR.Core.DTOs.Images;
using CR.Core.DTOs.RequestDTOs.Chat;
using CR.Core.DTOs.RequestDTOs.ChatUser;
using CR.Core.Services.Interfaces.ChatMessages;
using CR.Core.Services.Interfaces.ChatUsers;
using CR.Core.Services.Interfaces.Images;
using Microsoft.AspNetCore.Mvc;
using System;

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
        , IImageUploaderService imageUploaderService
        , ICheckForAppointmentTimeService checkForAppointmentTimeService)
        {
            _checkForAppointmentTimeService = checkForAppointmentTimeService;
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
                dateTime = $"{FixMessageTime(DateTime.Now.Minute)} : {FixMessageTime(DateTime.Now.Hour)}"
            };


            if (request.file != null)
            {
                var filePath = _imageUploaderService.Execute(new UploadImageDto()
                {
                    File = request.file,
                    Folder = "Chat"
                });

                result.Data = filePath;

                return new JsonResult(result);
            }

            result = _checkForAppointmentTimeService.Execute(new RequestCheckForAppointmentTimeDto()
            {
                chatUserId = request.chatUserId,
                file = null,
                message = request.message,
            });

            return new JsonResult(result);

        }

        [Route("/api/Chat/AddNewVoiceMessage")]
        [HttpPost]
        public IActionResult AddNewVoiceMessage([FromForm] RequestUploadVoiceDto request)
        {
            var result = new ResultDto<string>()
            {
                IsSuccess = true,
                dateTime = $"{FixMessageTime(DateTime.Now.Minute)} : {FixMessageTime(DateTime.Now.Hour)}"
            };

            result = _checkForAppointmentTimeService.Execute(new RequestCheckForAppointmentTimeDto()
            {
                chatUserId = request.chatUserId,
                file = null,
                message = "Voice"
            });

            if (result.IsSuccess == false)
            {
                return new JsonResult(result);
            }

            var filePath = _imageUploaderService.Execute(new UploadImageDto()
            {
                File = request.file,
                Folder = "ChatVoices"
            });

            result.Data = filePath;

            return new JsonResult(result);
        }

        private string FixMessageTime(int time)
        {
            string output = time.ToString();
            if (time < 10)
                output = "0" + time;

            return output;
        }
    }
}
