using CR.Core.DTOs.ExpertAvailabilities;
using System.Collections.Generic;

namespace CR.Presentation.Areas.ExpertPanel.Models.ViewModels
{
    public class ExpertAvailabilitiesViewModel
    {
        public List<DayDto> DayDtos { get; set; }
        public long ExpertInformationId { get; set; }
        public long ExpertId { get; set; }
    }
}
