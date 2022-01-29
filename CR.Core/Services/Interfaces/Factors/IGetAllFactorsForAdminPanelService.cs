using CR.Common.DTOs;
using CR.Core.DTOs.ResultDTOs;

namespace CR.Core.Services.Interfaces.Factors
{
    public interface IGetAllFactorsForAdminPanelService
    {
        ResultDto<ResultGetAllFactorsForAdminPanelDto> Execute(int Page = 1, int PageSize = 20);
    }
}
