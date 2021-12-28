using CR.Common.DTOs;
using CR.Core.DTOs.ResultDTOs;

namespace CR.Core.Services.Interfaces.Specialites
{
    public interface IGetAllSpecialitiesService
    {
        ResultDto<ResultGetAllSpecialitiesDto> Execute(int Page = 1, int PageSize = 20);
    }
}
