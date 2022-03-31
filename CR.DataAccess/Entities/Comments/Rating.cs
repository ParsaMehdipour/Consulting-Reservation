using CR.DataAccess.Common.Entity;
using CR.DataAccess.Entities.Users;

namespace CR.DataAccess.Entities.Comments
{
    public class Rating : BaseEntity
    {
        public int Rate { get; set; }

        #region ForeignKeys

        public long? UserId { get; set; }

        public long CommentId { get; set; }

        #endregion

        #region Navigation Property

        public User User { get; set; }
        public Comment Comment { get; set; }

        #endregion

    }
}
