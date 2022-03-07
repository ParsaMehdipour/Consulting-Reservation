using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.ChatUsers;
using CR.Core.Services.Interfaces.ChatUsers;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CR.Core.Services.Implementations.ChatUsers
{
    public class GetConsumerChatUsersService : IGetConsumerChatUsersService
    {
        private readonly ApplicationContext _context;

        public GetConsumerChatUsersService(ApplicationContext context)
        {
            _context = context;
        }
        public ResultDto<List<ChatUserForConsumerDto>> Execute(long consumerId, string searchKey)
        {
            var chatUsers = _context.ChatUsers
                .Include(_ => _.ExpertInformation)
                .Include(_ => _.Consumer)
                .Include(_ => _.ChatUserMessages)
                .Where(_ => _.ConsumerId == consumerId);

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                chatUsers = chatUsers.Where(_ => _.Consumer.FirstName.Contains(searchKey) || _.Consumer.LastName.Contains(searchKey));
            }

            var finalResult = chatUsers.Select(_ => new ChatUserForConsumerDto()
            {
                Id = _.Id,
                ExpertIconSrc = _.ExpertInformation.IconSrc,
                ExpertName = _.ExpertInformation.FirstName + " " + _.ExpertInformation.LastName,
                LastChangeHour = $"{_.ChatUserMessages.OrderByDescending(message => message.CreateDate).LastOrDefault().CreateDate.Minute.ToString().GetPersianNumber()} : {_.ChatUserMessages.OrderByDescending(message => message.CreateDate).LastOrDefault().CreateDate.Hour.ToString().GetPersianNumber()}",
                LastMessage = _.ChatUserMessages.OrderByDescending(message => message.CreateDate).LastOrDefault().Message,
                NotReadMessagesCount = _.ChatUserMessages.Count(chatUserMessage => chatUserMessage.IsRead == false),
            }).ToList();

            return new ResultDto<List<ChatUserForConsumerDto>>()
            {
                IsSuccess = true,
                Data = finalResult
            };
        }
    }
}
