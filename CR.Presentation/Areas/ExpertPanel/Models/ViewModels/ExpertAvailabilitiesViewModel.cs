using System.Collections.Generic;
using CR.Core.DTOs.ExpertAvailabilities;

namespace CR.Presentation.Areas.ExpertPanel.Models.ViewModels
{
    public class ExpertAvailabilitiesViewModel
    {
        public List<DayDto> DayDtos { get; set; }
        public long ExpertInformationId { get; set; }
    }
}
