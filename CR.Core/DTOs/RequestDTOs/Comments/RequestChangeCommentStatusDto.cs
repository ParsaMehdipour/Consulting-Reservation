using CR.DataAccess.Enums;

namespace CR.Core.DTOs.RequestDTOs.Comments
{
    public class RequestChangeCommentStatusDto
    {
        public long id { get; set; }
        public CommentStatus status { get; set; }
    }
}
