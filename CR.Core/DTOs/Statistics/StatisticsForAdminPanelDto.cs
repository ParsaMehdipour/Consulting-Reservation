namespace CR.Core.DTOs.Statistics
{
    public class StatisticsForAdminPanelDto
    {
        public string ConsumerCount { get; set; }
        public string ExpertCount { get; set; }
        public string AppointmentCount { get; set; }
        public string Income { get; set; }
        public StatisticsCountForAdminPanelDto ConsumerStatisticsCountForAdminPanelDto { get; set; }
        public StatisticsCountForAdminPanelDto ExpertStatisticsCountForAdminPanelDto { get; set; }
        public StatisticsAmountForAdminPanelDto IncomeStatisticsForAdminPanelDto { get; set; }
    }
}
