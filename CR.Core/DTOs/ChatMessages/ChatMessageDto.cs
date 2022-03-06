using CR.DataAccess.Enums;

namespace CR.Core.DTOs.ChatMessages
{
    public class ChatMessageDto
    {
        public string message { get; set; }
        public string expertIconSrc { get; set; }
        public string consumerIconSrc { get; set; }
        public string messageHour { get; set; }
        public MessageFlag messageFlag { get; set; }
    }
}
