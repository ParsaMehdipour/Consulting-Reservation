using CR.DataAccess.Enums;

namespace CR.Core.DTOs.RequestDTOs.Comments
{
    public class RequestAddNewCommentDto
    {
        public long userId { get; set; }
        public string message { get; set; }
        public CommentType typeId { get; set; }
        public long ownerRecordId { get; set; }
        public long? parentId { get; set; }
    }
}
