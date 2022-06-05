using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.Links;
using CR.Core.DTOs.ResultDTOs.Links;
using CR.DataAccess.Entities.Links;
using System.Collections.Generic;

namespace CR.Core.Services.Interfaces.Links
{
    public interface ILinkServices
    {
        ResultDto<ResultGetLinksForAdminPanelDto> GetAllLinksForAdminPanel(string searchKey, int Page = 1, int PageSize = 20, int ParentId = 0);
        ResultDto<List<Link>> GetAllLinksForSite();
        ResultDto AddNewLink(RequestAddNewLinkDto request);
        ResultDto DeleteLink(long linkId);
        ResultDto EditLink(RequestEditLinkDto request);
        ResultDto<RequestEditLinkDto> GetLink(long id);
    }
}
