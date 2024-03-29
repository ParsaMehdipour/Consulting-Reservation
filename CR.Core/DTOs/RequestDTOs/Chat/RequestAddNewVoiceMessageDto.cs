﻿using CR.DataAccess.Enums;

namespace CR.Core.DTOs.RequestDTOs.Chat
{
    public class RequestAddNewVoiceMessageDto
    {
        public long chatUserId { get; set; }
        public string filePath { get; set; }
        public MessageFlag messageFlag { get; set; }
    }
}
