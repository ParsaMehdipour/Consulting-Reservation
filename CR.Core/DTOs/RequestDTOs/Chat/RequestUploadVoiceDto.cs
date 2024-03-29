﻿using CR.DataAccess.Enums;
using Microsoft.AspNetCore.Http;

namespace CR.Core.DTOs.RequestDTOs.Chat
{
    public class RequestUploadVoiceDto
    {
        public long chatUserId { get; set; }
        public IFormFile file { get; set; }
        public MessageFlag messageFlag { get; set; }
    }
}
