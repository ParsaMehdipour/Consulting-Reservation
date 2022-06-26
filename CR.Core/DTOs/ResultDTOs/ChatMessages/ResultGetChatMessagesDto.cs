using CR.Core.DTOs.ChatMessages;
using System.Collections.Generic;

namespace CR.Core.DTOs.ResultDTOs.ChatMessages
{
    public class ResultGetChatMessagesDto
    {
        public List<ChatMessageDto> chatMessageDtos { get; set; }
        public string receiverIconSrc { get; set; }
        public string receiverFullName { get; set; }
        public bool isVoice { get; set; }
        public int NotReadCountExpertMessage { get; set; }
        public int NotReadCountConsumerMessage { get; set; }
        public string dateTime { get; set; }
    }
}
