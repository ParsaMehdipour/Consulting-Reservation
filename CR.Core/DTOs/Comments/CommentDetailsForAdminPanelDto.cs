using CR.DataAccess.Enums;

namespace CR.Core.DTOs.Comments
{
    public class CommentDetailsForAdminPanelDto
    {
        public string commenterFullName { get; set; }
        public string message { get; set; }
        public string status { get; set; }
        public string createDate { get; set; }
        public CommentType commentType { get; set; }

    }
}
