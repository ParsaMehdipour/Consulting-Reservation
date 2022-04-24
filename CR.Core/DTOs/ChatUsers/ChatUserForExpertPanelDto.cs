namespace CR.Core.DTOs.ChatUsers
{
    public class ChatUserForExpertPanelDto
    {
        public long Id { get; set; }
        public string ConsumerName { get; set; }
        public string ConsumerIconSrc { get; set; }
        public string LastMessage { get; set; }
        public int NotReadMessagesCount { get; set; }
        public string AppointmentDate { get; set; }
        public string MessageType { get; set; }
        public bool OnlineStatus { get; set; }

    }
}
