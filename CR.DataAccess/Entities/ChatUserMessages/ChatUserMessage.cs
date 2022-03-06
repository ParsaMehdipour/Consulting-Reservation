using CR.DataAccess.Common.Entity;
using CR.DataAccess.Entities.ChatUsers;
using CR.DataAccess.Enums;

namespace CR.DataAccess.Entities.ChatUserMessages
{
    public class ChatUserMessage : BaseEntity
    {

        public string Message { get; set; }
        public string File { get; set; }
        public MessageFlag MessageFlag { get; set; }
        public bool IsRead { get; set; }

        #region Foreign Keys

        public long ChatUserId { get; set; }

        #endregion

        #region Navigation Properties

        public ChatUser ChatUser { get; set; }

        #endregion
    }
}
