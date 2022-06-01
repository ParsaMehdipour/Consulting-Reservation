﻿using CR.Common.DTOs;
using CR.Core.DTOs.Notification;
using CR.Core.Services.Interfaces.Notification;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using System.Linq;

namespace CR.Core.Services.Implementations.Notification
{
    public class NotificationServices : INotificationServices
    {
        private readonly ApplicationContext _context;

        public NotificationServices(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<NotificationDTO> GetAllNotificationForAdminPanel()
        {
            var blogComments = _context.Comments.Where(_ => _.IsRead == false && _.TypeId == CommentType.Blog).Count();
            var expertComments = _context.Comments.Where(_ => _.IsRead == false && _.TypeId == CommentType.Expert).Count();
            var experts = _context.Users.Where(_ => _.IsActive == false && _.UserFlag == UserFlag.Expert).Count();
            var consumers = _context.Users.Where(_ => _.IsActive == false && _.UserFlag == UserFlag.Consumer).Count();

            return new ResultDto<NotificationDTO>()
            {
                Data = new NotificationDTO()
                {
                    notreadallcomments = (blogComments + expertComments).ToString(),
                    notreadblogcomments = blogComments.ToString(),
                    notreadcomments = expertComments.ToString(),
                    notcheckedallusers = (consumers + experts).ToString(),
                    notcheckedconsumers = consumers.ToString(),
                    notcheckedexperts = experts.ToString(),
                },
                IsSuccess = true
            };
        }

    }
}