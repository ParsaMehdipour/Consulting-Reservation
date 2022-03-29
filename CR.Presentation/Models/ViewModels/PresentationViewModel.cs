using CR.Core.DTOs.Blogs;
using CR.Core.DTOs.Comments;
using CR.Core.DTOs.Experts;
using CR.Core.DTOs.Specialities;
using System.Collections.Generic;

namespace CR.Presentation.Models.ViewModels
{
    public class PresentationViewModel
    {
        public List<SpecialityDto> SpecialityDtos { get; set; }
        public List<ExpertForPresentationDto> ExpertForPresentationDtos { get; set; }
        public List<BlogForPresentationDto> BlogForPresentationDtos { get; set; }
        public List<CommentForMainViewDto> CommentForMainViewDtos { get; set; }
    }
}
