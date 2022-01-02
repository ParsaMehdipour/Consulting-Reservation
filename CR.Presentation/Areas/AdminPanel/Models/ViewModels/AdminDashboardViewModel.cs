using System.Collections.Generic;
using CR.Core.DTOs.Appointment;
using CR.Core.DTOs.Consumers;
using CR.Core.DTOs.Experts;
using CR.Core.DTOs.Statistics;

namespace CR.Presentation.Areas.AdminPanel.Models.ViewModels
{
    public class AdminDashboardViewModel
    {
        public List<LatestExpertForAdminDto> LatestExpertForAdminDtos { get; set; }
        public List<LatestConsumerForAdminDto> LatestConsumerForAdminDtos { get; set; }
        public List<AppointmentForAdminDto> AppointmentForAdminDtos { get; set; }
        public StatisticsForAdminPanelDto StatisticsDto { get; set; }
    }
}
