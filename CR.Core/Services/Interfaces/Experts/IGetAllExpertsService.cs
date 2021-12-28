using CR.Common.DTOs;
using CR.Core.DTOs.ResultDTOs;

namespace CR.Core.Services.Interfaces.Experts
{
    public interface IGetAllExpertsService
    {
        ResultDto<ResultGetAllExpertsDto> Execute(int Page = 1, int PageSize = 20);
    }
}
