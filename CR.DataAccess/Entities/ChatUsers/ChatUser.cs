using CR.DataAccess.Common.Entity;
using CR.DataAccess.Entities.Appointments;
using CR.DataAccess.Entities.ChatUserMessages;
using CR.DataAccess.Entities.IndividualInformations;
using CR.DataAccess.Entities.Users;
using CR.DataAccess.Enums;
using System;
using System.Collections.Generic;

namespace CR.DataAccess.Entities.ChatUsers
{
    public class ChatUser : BaseEntity
    {
        public DateTime lastChangeDate { get; set; } = DateTime.Now;
        public CallingType MessageType { get; set; }
        public ChatStatus ChatStatus { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentDate_String { get; set; }

        #region Foreign Keys

        public long ConsumerId { get; set; }
        public long ExpertInformationId { get; set; }
        public long AppointmentId { get; set; }

        #endregion

        #region Navigation Properties

        public User Consumer { get; set; }
        public Appointment Appointment { get; set; }
        public ExpertInformation ExpertInformation { get; set; }
        public ICollection<ChatUserMessage> ChatUserMessages { get; set; }

        #endregion
    }
}
