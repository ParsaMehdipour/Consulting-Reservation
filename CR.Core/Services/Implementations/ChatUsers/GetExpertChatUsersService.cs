﻿using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.ChatUsers;
using CR.Core.Services.Interfaces.ChatUsers;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CR.Core.Services.Implementations.ChatUsers
{
    public class GetExpertChatUsersService : IGetExpertChatUsersService
    {
        private readonly ApplicationContext _context;

        public GetExpertChatUsersService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<List<ChatUserForExpertPanelDto>> Execute(long expertId, string searchKey)
        {
            var expert = _context.Users.Find(expertId);

            var chatUsers = _context.ChatUsers
                .Include(_ => _.Consumer)
                .Include(_ => _.ChatUserMessages)
                .Where(_ => _.ExpertInformationId == expert.ExpertInformationId);

            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                chatUsers = chatUsers.Where(_ => _.Consumer.FirstName.Contains(searchKey) || _.Consumer.LastName.Contains(searchKey));
            }

            var finalResult = chatUsers.Select(_ => new ChatUserForExpertPanelDto()
            {
                Id = _.Id,
                ConsumerIconSrc = (string.IsNullOrWhiteSpace(_.Consumer.IconSrc)) ? "assets/img/icon-256x256.png" : _.Consumer.IconSrc,
                ConsumerName = _.Consumer.FirstName + " " + _.Consumer.LastName,
                LastChangeHour = $"{_.ChatUserMessages.OrderBy(c => c.CreateDate).Last().CreateDate.Minute.ToString().GetPersianNumber()} : {_.ChatUserMessages.OrderBy(c => c.CreateDate).Last().CreateDate.Hour.ToString().GetPersianNumber()}",
                LastMessage = _.ChatUserMessages.OrderBy(c => c.CreateDate).Last().Message,
                NotReadMessagesCount = _.ChatUserMessages.Count(chatUserMessage => chatUserMessage.IsRead == false),
            }).ToList();

            return new ResultDto<List<ChatUserForExpertPanelDto>>()
            {
                IsSuccess = true,
                Data = finalResult
            };
        }
    }
}
