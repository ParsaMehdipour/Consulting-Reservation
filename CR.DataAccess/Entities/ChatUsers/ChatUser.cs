using CR.DataAccess.Common.Entity;
using CR.DataAccess.Entities.ChatUserMessages;
using CR.DataAccess.Entities.IndividualInformations;
using CR.DataAccess.Entities.Users;
using System;
using System.Collections.Generic;

namespace CR.DataAccess.Entities.ChatUsers
{
    public class ChatUser : BaseEntity
    {

        public DateTime lastChangeDate { get; set; } = DateTime.Now;

        #region Foreign Keys

        public long ConsumerId { get; set; }
        public long ExpertInformationId { get; set; }

        #endregion

        #region Navigation Properties

        public User Consumer { get; set; }
        public ExpertInformation ExpertInformation { get; set; }
        public ICollection<ChatUserMessage> ChatUserMessages { get; set; }

        #endregion
    }
}
