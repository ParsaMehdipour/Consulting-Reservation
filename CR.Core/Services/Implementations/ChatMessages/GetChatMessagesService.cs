using CR.Common.DTOs;
using CR.Core.DTOs.ChatMessages;
using CR.Core.DTOs.ResultDTOs.ChatMessages;
using CR.Core.Services.Interfaces.ChatMessages;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CR.Core.Services.Implementations.ChatMessages
{
    public class GetChatMessagesService : IGetChatMessagesService
    {
        private readonly ApplicationContext _context;

        public GetChatMessagesService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetChatMessagesDto> Execute(long chatUserId, bool isExpert)
        {
            var chatUser = _context.ChatUsers.Include(_ => _.Consumer)
                .Include(_ => _.ExpertInformation)
                .Include(_ => _.Appointment)
                .FirstOrDefault(_ => _.Id == chatUserId);

            if (chatUser == null)
            {
                return new ResultDto<ResultGetChatMessagesDto>()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "کاربر یافت نشد!!"
                };
            }

            var chatMessages = _context.ChatUserMessages
                .Include(_ => _.ChatUser)
                .ThenInclude(_ => _.Consumer)
                .ThenInclude(_ => _.ExpertInformation)
                .Where(_ => _.ChatUserId == chatUserId).ToList();

            var result = new List<ChatMessageDto>();

            foreach (var chatUserMessage in chatMessages)
            {
                var chatMessageDto = new ChatMessageDto()
                {
                    consumerIconSrc = string.IsNullOrWhiteSpace(chatUserMessage.ChatUser.Consumer.IconSrc) ? "assets/img/icon-256x256.png" : chatUserMessage.ChatUser.Consumer.IconSrc,
                    expertIconSrc = chatUserMessage.ChatUser.ExpertInformation.IconSrc,
                    message = chatUserMessage.Message,
                    messageFlag = chatUserMessage.MessageFlag,
                    messageHour = $"{FixMessageTime(chatUserMessage.CreateDate.Minute)} : {FixMessageTime(chatUserMessage.CreateDate.Hour)}",
                    hasFile = (!string.IsNullOrWhiteSpace(chatUserMessage.File)),
                    file = chatUserMessage.File,
                    hasAudio = (!string.IsNullOrWhiteSpace(chatUserMessage.Audio)),
                    audio = chatUserMessage.Audio
                };

                if (isExpert)
                {
                    if (chatUserMessage.MessageFlag == MessageFlag.ConsumerMessage)
                    {
                        chatUserMessage.IsRead = true;
                    }
                }
                else
                {
                    if (chatUserMessage.MessageFlag == MessageFlag.ExpertMessage)
                    {
                        chatUserMessage.IsRead = true;
                    }
                }

                result.Add(chatMessageDto);
            }

            _context.SaveChanges();

            return new ResultDto<ResultGetChatMessagesDto>()
            {
                Data = new ResultGetChatMessagesDto()
                {
                    isVoice = chatUser.Appointment.CallingType == CallingType.VoiceCall,
                    chatMessageDtos = result,
                    receiverFullName = (isExpert) ? chatUser.Consumer.FirstName + " " + chatUser.Consumer.LastName : chatUser.ExpertInformation.FirstName + " " + chatUser.ExpertInformation.LastName,
                    receiverIconSrc = (isExpert) ? (string.IsNullOrWhiteSpace(chatUser.Consumer.IconSrc) ? "assets/img/icon-256x256.png" : chatUser.Consumer.IconSrc) : (string.IsNullOrWhiteSpace(chatUser.ExpertInformation.IconSrc) ? "assets/img/icon-256x256.png" : chatUser.ExpertInformation.IconSrc)
                },
                IsSuccess = true
            };

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
