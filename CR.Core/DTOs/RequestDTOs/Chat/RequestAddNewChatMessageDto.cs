using CR.DataAccess.Enums;

namespace CR.Core.DTOs.RequestDTOs.Chat
{
    public class RequestAddNewChatMessageDto
    {
        public long chatUserId { get; set; }

        public string message { get; set; }

        public string filePath { get; set; }

        public MessageFlag messageFlag { get; set; }
    }
}
