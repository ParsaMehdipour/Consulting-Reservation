using CR.Core.DTOs.Statistics;

namespace CR.Core.Services.Interfaces.Statistics
{
    public interface IGetStatisticsForExpertPanelService
    {
        StatisticsForExpertPanelDto Execute(long expertId);
    }
}
