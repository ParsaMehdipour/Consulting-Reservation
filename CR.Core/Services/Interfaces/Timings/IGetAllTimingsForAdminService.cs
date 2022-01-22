using System.Collections.Generic;
using CR.Common.DTOs;
using CR.Core.DTOs.ResultDTOs;

namespace CR.Core.Services.Interfaces.Timings
{
    public interface IGetAllTimingsForAdminService
    {
        ResultDto<ResultGetAllTimingsForAdminPanelDto> Execute(int Page = 1, int PageSize = 20);
    }
}
