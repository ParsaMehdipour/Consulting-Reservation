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
                .ThenInclude(_ => _.Expert)
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
                ExpertIconSrc = (string.IsNullOrWhiteSpace(_.ExpertInformation.IconSrc)) ? "assets/img/icon-256x256.png" : _.ExpertInformation.IconSrc,
                ExpertName = _.ExpertInformation.FirstName + " " + _.ExpertInformation.LastName,
                AppointmentDate = _.AppointmentDate_String,
                LastMessage = _.ChatUserMessages.OrderBy(_ => _.CreateDate).Last().Message,
                NotReadMessagesCount = _.ChatUserMessages.Count(chatUserMessage => chatUserMessage.IsRead == false),
                MessageType = _.MessageType.GetDisplayName(),
                OnlineFlag = _.ExpertInformation.Expert.OnlineFlag
            }).ToList();

            return new ResultDto<List<ChatUserForConsumerDto>>()
            {
                IsSuccess = true,
                Data = finalResult
            };
        }
    }
}
