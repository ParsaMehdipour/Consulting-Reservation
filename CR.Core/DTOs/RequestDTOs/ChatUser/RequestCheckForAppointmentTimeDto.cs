using Microsoft.AspNetCore.Http;

namespace CR.Core.DTOs.RequestDTOs.ChatUser
{
    public class RequestCheckForAppointmentTimeDto
    {
        public long chatUserId { get; set; }
        public string message { get; set; }
        public IFormFile file { get; set; }
    }
}
