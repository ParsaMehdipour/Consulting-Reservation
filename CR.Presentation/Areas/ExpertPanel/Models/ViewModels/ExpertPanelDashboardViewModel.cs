using CR.Core.DTOs.ResultDTOs;
using CR.Core.DTOs.ResultDTOs.Consumers;
using CR.Core.DTOs.Statistics;

namespace CR.Presentation.Areas.ExpertPanel.Models.ViewModels
{
    public class ExpertPanelDashboardViewModel
    {
        public ResultGetConsumersForExpertDashboardDto ResultGetTodayConsumersForExpertDashboardDto { get; set; }
        public ResultGetConsumersForExpertDashboardDto ResultGetIncomingConsumersForExpertDashboardDto { get; set; }
        public StatisticsForExpertPanelDto StatisticsForExpertPanelDto { get; set; }
    }
}
