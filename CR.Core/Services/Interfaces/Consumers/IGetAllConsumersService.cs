using CR.Common.DTOs;
using CR.Core.DTOs.ResultDTOs;

namespace CR.Core.Services.Interfaces.Consumers
{
    public interface IGetAllConsumersService
    {
        ResultDto<ResultGetAllConsumersDto> Execute(int Page = 1, int PageSize = 20);
    }
}
