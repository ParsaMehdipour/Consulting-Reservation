using System.Collections.Generic;
using CR.Core.DTOs.ExpertAvailabilities;
using CR.DataAccess.Entities.ExpertAvailabilities;

namespace CR.Core.DTOs.RequestDTOs
{
    public class RequestAddNewTimeOfDayDto
    {
        public long dayId { get; set; }
        public long expertInformationId { get; set; }
        //public List<TimeOfDayDto> TimeOfDays { get; set; }
        public string[] start { get; set; }
        public string[] finish { get; set; }
    }
}
