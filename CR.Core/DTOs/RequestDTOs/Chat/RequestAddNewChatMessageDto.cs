using CR.DataAccess.Enums;
using Microsoft.AspNetCore.Http;

namespace CR.Core.DTOs.RequestDTOs.Chat
{
    public class RequestAddNewChatMessageDto
    {
        public long chatUserId { get; set; }

        public string message { get; set; }

        public IFormFile file { get; set; }

        public MessageFlag messageFlag { get; set; }
    }
}
