using CR.DataAccess.Enums;

namespace CR.Core.DTOs.RequestDTOs.Comments
{
    public class RequestAddNewReplyDto
    {
        public string message { get; set; }
        public CommentType typeId { get; set; }
        public long parentId { get; set; }
    }
}
