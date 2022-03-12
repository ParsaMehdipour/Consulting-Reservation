using CR.Common.DTOs;
using CR.Core.DTOs.ResultDTOs.ContactUs;

namespace CR.Core.Services.Interfaces.ContactUs
{
    public interface IGetContactUsForAdminPanelService
    {
        ResultDto<ResultGetContactUsDto> Execute(int Page = 1, int PageSize = 20);
    }
}
