using CR.Core.DTOs.ChatMessages;
using System.Collections.Generic;

namespace CR.Core.DTOs.ResultDTOs.ChatMessages
{
    public class ResultGetChatMessagesDto
    {
        public List<ChatMessageDto> chatMessageDtos { get; set; }
        public string receiverIconSrc { get; set; }
        public string receiverFullName { get; set; }
    }
}
