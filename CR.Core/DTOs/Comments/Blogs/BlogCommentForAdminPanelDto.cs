namespace CR.Core.DTOs.Comments.Blogs
{
    public class BlogCommentForAdminPanelDto
    {
        public long Id { get; set; }
        public string CommenterIconSrc { get; set; }
        public string CommenterFullName { get; set; }
        public long CommenterId { get; set; }
        public string BlogName { get; set; }
        public string BlogIconSrc { get; set; }
        public long BlogId { get; set; }
        public string Message { get; set; }
        public string CreateDate { get; set; }
        public string Status { get; set; }
        public bool IsRead { get; set; }
    }
}
