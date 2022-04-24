using CR.Common.Utilities;
using CR.Core.DTOs.RequestDTOs.Chat;
using CR.Core.Services.Interfaces.ChatMessages;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace CR.Presentation.Hubs
{
    public class SiteChatHub : Hub
    {
        private readonly IAddNewChatMessageService _addNewChatMessageService;
        private readonly ApplicationContext _context;

        public SiteChatHub(IAddNewChatMessageService addNewChatMessageService
        , ApplicationContext context)
        {
            _addNewChatMessageService = addNewChatMessageService;
            _context = context;
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
                await Clients.User(result.Data.userId).SendAsync("ReceiveMessageHandler", message, messageFlag, result.Data.messageHour, filePath);
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

        public override Task OnDisconnectedAsync(Exception exception)
        {

            var userId = ClaimUtility.GetUserId(Context.User).Value;

            if (userId != 0)
            {
                var user = _context.Users.Find(userId);

                user.OnlineFlag = false;

                _context.SaveChanges();
            }

            return base.OnDisconnectedAsync(exception);
        }
    }
}
