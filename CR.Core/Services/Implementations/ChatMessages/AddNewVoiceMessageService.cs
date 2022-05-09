using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.Chat;
using CR.Core.DTOs.ResultDTOs.ChatMessages;
using CR.Core.Services.Interfaces.ChatMessages;
using CR.Core.Services.Interfaces.Images;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.ChatUserMessages;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CR.Core.Services.Implementations.ChatMessages
{
    public class AddNewVoiceMessageService : IAddNewVoiceMessageService
    {
        private readonly ApplicationContext _context;
        private readonly IImageUploaderService _imageUploaderService;

        public AddNewVoiceMessageService(ApplicationContext context
        , IImageUploaderService imageUploaderService)
        {
            _context = context;
            _imageUploaderService = imageUploaderService;
        }

        public ResultDto<ResultAddChatMessageDto> Execute(RequestAddNewVoiceMessageDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                string userId = "";
                bool onlineStatus = false;

                var chatUser = _context.ChatUsers
                    .Include(_ => _.Appointment)
                    .ThenInclude(_ => _.TimeOfDay)
                    .FirstOrDefault(_ => _.Id == request.chatUserId);

                if (chatUser.ChatStatus == ChatStatus.Waiting && request.messageFlag == MessageFlag.ExpertMessage)
                {
                    chatUser.ChatStatus = ChatStatus.Started;

                    _context.SaveChanges();
                }

                var chatMessage = new ChatUserMessage()
                {
                    ChatUserId = request.chatUserId,
                    ChatUser = chatUser,
                    MessageFlag = request.messageFlag,
                    Audio = request.filePath
                };

                if (request.messageFlag == MessageFlag.ConsumerMessage)
                {
                    userId = _context.ChatUsers.Include(_ => _.ExpertInformation)
                        .FirstOrDefault(_ => _.Id == request.chatUserId)?.ExpertInformation.ExpertId.ToString();

                    onlineStatus = _context.Users.Find(Convert.ToInt64(userId)).OnlineFlag;
                }
                else
                {
                    userId = _context.ChatUsers.Include(_ => _.Consumer)
                        .FirstOrDefault(_ => _.Id == request.chatUserId)?.Consumer.Id.ToString();

                    onlineStatus = _context.Users.Find(Convert.ToInt64(userId)).OnlineFlag;

                }

                _context.ChatUserMessages.Add(chatMessage);

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto<ResultAddChatMessageDto>()
                {
                    IsSuccess = true,
                    Message = string.Empty,
                    Data = new ResultAddChatMessageDto()
                    {
                        userId = userId,
                        messageHour = $"{chatMessage.CreateDate.Minute} : {chatMessage.CreateDate.Hour}",
                        onlineStatus = onlineStatus
                    }
                };

            }
            catch (Exception)
            {
                transaction.Rollback();

                return new ResultDto<ResultAddChatMessageDto>()
                {
                    IsSuccess = false,
                    Message = "خطا از سمت سرور",
                    Data = null
                };
            }
        }
    }
}
