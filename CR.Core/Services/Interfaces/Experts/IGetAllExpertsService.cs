using CR.Common.DTOs;
using CR.Core.DTOs.ResultDTOs;
using CR.Core.DTOs.ResultDTOs.Experts;

namespace CR.Core.Services.Interfaces.Experts
{
    public interface IGetAllExpertsService
    {
        ResultDto<ResultGetAllExpertsDto> Execute(int Page = 1, int PageSize = 20);
    }
}
