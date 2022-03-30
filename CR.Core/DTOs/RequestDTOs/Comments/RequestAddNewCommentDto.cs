using CR.DataAccess.Enums;

namespace CR.Core.DTOs.RequestDTOs.Comments
{
    public class RequestAddNewCommentDto
    {
        public string message { get; set; }
        public CommentType typeId { get; set; }
        public long ownerRecordId { get; set; }
        public long? parentId { get; set; }
        public int? rate { get; set; }
    }
}
