using System.Collections.Generic;
using CR.Core.DTOs.Experts;
using CR.Core.DTOs.Specialities;

namespace CR.Presentation.Models.ViewModels
{
    public class PresentationViewModel
    {
        public List<SpecialityDto> SpecialityDtos { get; set; }
        public List<ExpertForPresentationDto> ExpertForPresentationDtos { get; set; }
    }
}
