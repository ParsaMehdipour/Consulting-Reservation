using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Core.DTOs.ChatMessages;
using CR.Core.DTOs.ResultDTOs.ChatMessages;
using CR.Core.Services.Interfaces.ChatMessages;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
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
            var chatUser = _context.ChatUsers.Include(_ => _.Consumer).Include(_ => _.ExpertInformation).FirstOrDefault(_ => _.Id == chatUserId);

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
                .Where(_ => _.ChatUserId == chatUserId)
                .Select(_ => new ChatMessageDto()
                {
                    consumerIconSrc = _.ChatUser.Consumer.IconSrc,
                    expertIconSrc = _.ChatUser.ExpertInformation.IconSrc,
                    message = _.Message,
                    messageFlag = _.MessageFlag,
                    messageHour = $"{_.CreateDate.Minute} : {_.CreateDate.Hour} - {_.CreateDate.ToShamsi()}"
                }).ToList();

            return new ResultDto<ResultGetChatMessagesDto>()
            {
                Data = new ResultGetChatMessagesDto()
                {
                    chatMessageDtos = chatMessages,
                    receiverFullName = (isExpert) ? chatUser.Consumer.FirstName + " " + chatUser.Consumer.LastName : chatUser.ExpertInformation.FirstName + " " + chatUser.ExpertInformation.LastName,
                    receiverIconSrc = (isExpert) ? chatUser.Consumer.IconSrc : chatUser.ExpertInformation.IconSrc
                },
                IsSuccess = true
            };

        }
    }
}
