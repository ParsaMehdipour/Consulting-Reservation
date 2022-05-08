namespace CR.Core.DTOs.ChatUsers
{
    public class ChatUserForConsumerDto
    {
        public long Id { get; set; }
        public string ExpertName { get; set; }
        public string ExpertIconSrc { get; set; }
        public string LastMessage { get; set; }
        public int NotReadMessagesCount { get; set; }
        public string AppointmentDate { get; set; }
        public string MessageType { get; set; }
        public string ChatStatus { get; set; }
        public bool OnlineFlag { get; set; }
        public string AppointmentTime { get; set; }
    }
}
