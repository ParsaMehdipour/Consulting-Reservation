using CR.Core.DTOs.RequestDTOs.Chat;
using CR.Core.Services.Interfaces.ChatMessages;
using CR.DataAccess.Enums;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace CR.Presentation.Hubs
{
    public class SiteChatHub : Hub
    {
        private readonly IAddNewChatMessageService _addNewChatMessageService;

        public SiteChatHub(IAddNewChatMessageService addNewChatMessageService)
        {
            _addNewChatMessageService = addNewChatMessageService;
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
            var s = Context.ConnectionId;
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
