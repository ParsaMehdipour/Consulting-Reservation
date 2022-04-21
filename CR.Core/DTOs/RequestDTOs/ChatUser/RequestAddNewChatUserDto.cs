using CR.DataAccess.Enums;
using System;

namespace CR.Core.DTOs.RequestDTOs.ChatUser
{
    public class RequestAddNewChatUserDto
    {
        public long expertInformationId { get; set; }
        public long consumerId { get; set; }
        public CallingType messageType { get; set; }
        public DateTime appointmentDate { get; set; }
        public long appointmentId { get; set; }
    }
}
