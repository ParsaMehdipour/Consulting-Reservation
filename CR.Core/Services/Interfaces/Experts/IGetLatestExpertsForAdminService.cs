using System.Collections.Generic;
using CR.Common.DTOs;
using CR.Core.DTOs.Experts;

namespace CR.Core.Services.Interfaces.Experts
{
    public interface IGetLatestExpertsForAdminService
    {
        ResultDto<List<LatestExpertForAdminDto>> Execute();
    }
}
