using CR.Core.DTOs.Experts;
using CR.Core.DTOs.ResultDTOs.Comments;

namespace CR.Presentation.Models.ViewModels
{
    public class ExpertProfileViewModel
    {
        public ExpertDetailsForSiteDto ExpertDetailsForSiteDto { get; set; }
        public ResultGetExpertCommentsForPresentationDto ResultGetExpertCommentsForPresentationDto { get; set; }
    }
}
