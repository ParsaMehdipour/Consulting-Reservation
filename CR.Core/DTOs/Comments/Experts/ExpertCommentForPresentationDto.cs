using System.Collections.Generic;

namespace CR.Core.DTOs.Comments.Experts
{
    public class ExpertCommentForPresentationDto
    {
        public long Id { get; set; }
        public string CommenterIconSrc { get; set; }
        public string CommenterFullName { get; set; }
        public string CreateDate { get; set; }
        public string Message { get; set; }
        public bool HasChildren { get; set; }
        public long ParentId { get; set; }
        public List<ExpertCommentForPresentationDto> Children { get; set; }
    }
}
