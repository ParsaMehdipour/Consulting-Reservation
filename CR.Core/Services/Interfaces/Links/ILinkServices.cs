using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.Links;
using CR.Core.DTOs.ResultDTOs.Links;

namespace CR.Core.Services.Interfaces.Links
{
    public interface ILinkServices
    {
        ResultDto<ResultGetLinksForAdminPanelDto> GetAllLinksForAdminPanel(string searchKey, int Page = 1, int PageSize = 20);
        ResultDto AddNewLink(RequestAddNewLinkDto request);
    }
}
