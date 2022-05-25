using CR.Common.Utilities;
using CR.Core.DTOs.RequestDTOs.Chat;
using CR.Core.Services.Interfaces.ChatMessages;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CR.Presentation.Hubs
{
    public class SiteChatHub : Hub
    {
        private readonly IAddNewChatMessageService _addNewChatMessageService;
        private readonly ApplicationContext _context;
        private readonly IAddNewVoiceMessageService _addNewVoiceMessageService;

        public SiteChatHub(IAddNewChatMessageService addNewChatMessageService
        , ApplicationContext context
        , IAddNewVoiceMessageService addNewVoiceMessageService)
        {
            _addNewChatMessageService = addNewChatMessageService;
            _context = context;
            _addNewVoiceMessageService = addNewVoiceMessageService;
        }

        public async Task SendMessage(long chatUserId, string message, MessageFlag messageFlag, string filePath)
        {
            var request = new RequestAddNewChatMessageDto()
            {
                chatUserId = chatUserId,
                message = message,
                messageFlag = messageFlag,
                filePath = filePath
            };

            var result = _addNewChatMessageService.Execute(request);

            if (result.IsSuccess)
            {
                await Clients.User(result.Data.userId).SendAsync("ReceiveMessageHandler", message, messageFlag, result.Data.messageHour, filePath, result.Data.NotReadCount, chatUserId);
            }
        }

        public async Task SendVoice(long chatUserId, MessageFlag messageFlag, string audioPath)
        {
            var request = new RequestAddNewVoiceMessageDto()
            {
                chatUserId = chatUserId,
                messageFlag = messageFlag,
                filePath = audioPath
            };

            var result = _addNewVoiceMessageService.Execute(request);

            if (result.IsSuccess)
            {
                await Clients.User(result.Data.userId).SendAsync("ReceiveVoiceHandler", messageFlag, result.Data.messageHour, audioPath, result.Data.NotReadCount, chatUserId);
            }
        }

        public override Task OnConnectedAsync()
        {
            var userId = ClaimUtility.GetUserId(Context.User).Value;

            if (userId != 0)
            {
                var user = _context.Users.Find(userId);

                user.OnlineFlag = true;

                _context.SaveChanges();
            }

            return base.OnConnectedAsync();
        }

        public override async Task<Task> OnDisconnectedAsync(Exception exception)
        {
            var userId = ClaimUtility.GetUserId(Context.User).Value;

            if (userId != 0)
            {
                var user = _context.Users.Find(userId);

                user.OnlineFlag = false;

                if (user.UserFlag == UserFlag.Expert)
                {
                    var expertChatUsers = _context.ChatUsers.Include(_ => _.Appointment).ThenInclude(_ => _.TimeOfDay).Where(_ => _.ExpertInformationId == user.ExpertInformationId).ToList();

                    foreach (var chatUser in expertChatUsers)
                    {
                        if (chatUser.ChatStatus == ChatStatus.Started && chatUser.Appointment.TimeOfDay.StartTime <= DateTime.Now)
                        {
                            var request = new RequestAddNewChatMessageDto()
                            {
                                chatUserId = chatUser.Id,
                                message = "مشاور خارج شد",
                                messageFlag = MessageFlag.ConsumerMessage,
                                filePath = null
                            };

                            var result = _addNewChatMessageService.Execute(request);

                            if (result.IsSuccess)
                            {
                                await Clients.User(result.Data.userId).SendAsync("ReceiveMessageHandler", request.message, request.messageFlag, result.Data.messageHour, null);
                            }
                        }
                    }
                }
                else
                {
                    var consumerChatUsers = _context.ChatUsers.Include(_ => _.Appointment).ThenInclude(_ => _.TimeOfDay).Where(_ => _.ConsumerId == user.Id).ToList();

                    foreach (var chatUser in consumerChatUsers)
                    {
                        if (chatUser.ChatStatus == ChatStatus.Started && chatUser.Appointment.TimeOfDay.StartTime <= DateTime.Now)
                        {
                            var request = new RequestAddNewChatMessageDto()
                            {
                                chatUserId = chatUser.Id,
                                message = "کاربر خارج شد",
                                messageFlag = MessageFlag.ConsumerMessage,
                                filePath = null
                            };

                            var result = _addNewChatMessageService.Execute(request);

                            if (result.IsSuccess)
                            {
                                await Clients.User(result.Data.userId).SendAsync("ReceiveMessageHandler", request.message, request.messageFlag, result.Data.messageHour, null);
                            }
                        }
                    }
                }

                _context.SaveChanges();

                //Clients.All.SendAsync("UserLogOut", user.UserFlag.GetDisplayName());
            }

            return base.OnDisconnectedAsync(exception);
        }
    }
}
