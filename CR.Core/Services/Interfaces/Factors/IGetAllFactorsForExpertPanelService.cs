using CR.Common.DTOs;
using CR.Core.DTOs.ResultDTOs;
using CR.Core.DTOs.ResultDTOs.Factors;

namespace CR.Core.Services.Interfaces.Factors
{
    public interface IGetAllFactorsForExpertPanelService
    {
        ResultDto<ResultGetAllFactorsForExpertPanelService> Execute(long expertId, int Page = 1, int PageSize = 20);
    }
}
