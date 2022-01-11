using System.Collections.Generic;
using CR.Core.DTOs.Experts;
using Microsoft.AspNetCore.Http;

namespace CR.Core.DTOs.RequestDTOs
{
    public class RequestEditAdvancedExpertDetailsDto
    {
        public long id { get; set; }
        public List<IFormFile> imageFile { get; set; }
        public List<string> imageName { get; set; }
        public List<ExpertExperienceDto> experiences { get; set; }
        public List<ExpertMembershipDto> memberships { get; set; }
        public List<ExpertPrizeDto> prizes { get; set; }
        public List<ExpertStudyDto> studies { get; set; }
        public List<ExpertSubscriptionDto> subscriptions { get; set; }
    }
}
