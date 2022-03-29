namespace CR.Core.DTOs.Comments.Experts
{
    public class ExpertCommentForAdminPanelDto
    {
        public long Id { get; set; }
        public string CommenterIconSrc { get; set; }
        public string CommenterFullName { get; set; }
        public long CommenterId { get; set; }
        public string ExpertFullName { get; set; }
        public string ExpertIconSrc { get; set; }
        public long ExpertId { get; set; }
        public string Message { get; set; }
        public string CreateDate { get; set; }
        public string Status { get; set; }
        public bool ShowStatus { get; set; }

    }
}
