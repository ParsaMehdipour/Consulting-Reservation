using CR.DataAccess.Common.Entity;
using CR.DataAccess.Entities.Users;
using CR.DataAccess.Enums;
using System.Collections.Generic;

namespace CR.DataAccess.Entities.Comments
{
    public class Comment : BaseEntity
    {
        public long UserId { get; set; }
        public string Message { get; set; }
        public CommentStatus CommentStatus { get; set; }
        public CommentType TypeId { get; set; }
        public long OwnerRecordId { get; set; }
        public bool IsRead { get; set; }
        public bool ShowInMainPage { get; set; }
        public long? ParentId { get; set; }

        #region Navigation Properties

        public User User { get; set; }
        public Comment Parent { get; set; }
        public ICollection<Comment> Children { get; set; }

        #endregion
    }
}
