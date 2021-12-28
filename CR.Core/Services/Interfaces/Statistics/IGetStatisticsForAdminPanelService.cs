using CR.Common.DTOs;
using CR.Core.DTOs.Statistics;

namespace CR.Core.Services.Interfaces.Statistics
{
    public interface IGetStatisticsForAdminPanelService
    {
        ResultDto<StatisticsForAdminPanelDto> Execute();
    }
}
