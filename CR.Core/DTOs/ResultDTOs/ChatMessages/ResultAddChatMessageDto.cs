﻿namespace CR.Core.DTOs.ResultDTOs.ChatMessages
{
    public class ResultAddChatMessageDto
    {
        public string userId { get; set; }
        public string messageHour { get; set; }
        public bool onlineStatus { get; set; }
        public int NotReadCount { get; set; }
    }
}
