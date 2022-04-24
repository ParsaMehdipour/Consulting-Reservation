using Microsoft.AspNetCore.Http;

namespace CR.Core.DTOs.RequestDTOs.Chat
{
    public class RequestAddNewMessageWithImageDto
    {
        public long chatUserId { get; set; }
        public string message { get; set; }
        public IFormFile file { get; set; }
    }
}
