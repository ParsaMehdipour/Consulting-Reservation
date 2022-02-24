using CR.Common.DTOs;
using CR.Core.DTOs.ResultDTOs.Experts;
using CR.DataAccess.Enums;
using System.Collections.Generic;

namespace CR.Core.Services.Interfaces.Experts
{
    public interface IGetExpertsForSiteService
    {
        ResultDto<ResultGetExpertsForSiteDto> Execute(string searchKey, List<string> speciality, GenderType gender, int Page = 1, int PageSize = 20);
    }
}
