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

        public async Task SendMessage(long user, string message, MessageFlag messageFlag)
        {
            //if (user == 0)
            //    await Clients.All.SendAsync("ReceiveMessageHandler", message);
            //else
            //    await Clients.User(user.ToString()).SendAsync("ReceiveMessageHandler", message);

            var request = new RequestAddNewChatMessageDto()
            {
                chatUserId = user,
                message = message,
                messageFlag = messageFlag
            };

            var result = _addNewChatMessageService.Execute(request);

            if (result.IsSuccess)
            {
                await Clients.User(user.ToString()).SendAsync("ReceiveMessageHandler", message);
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
