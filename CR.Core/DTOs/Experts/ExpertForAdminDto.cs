using System.Collections.Generic;
using CR.Core.DTOs.ExpertAvailabilities;

namespace CR.Core.DTOs.Experts
{
    public class ExpertForAdminDto
    {
        public long Id { get; set; }
        public string IconSrc { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Speciality { get; set; }
        public string RegisterDate { get; set; }
        public string Income { get; set; }
        public bool IsActive { get; set; }
        public List<TimeOfDayDto> TimeOfDayDtos { get; set; }
    }
}
