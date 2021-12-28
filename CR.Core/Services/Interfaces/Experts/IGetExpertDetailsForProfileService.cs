using CR.Common.DTOs;
using CR.Core.DTOs.Experts;

namespace CR.Core.Services.Interfaces.Experts
{
    public interface IGetExpertDetailsForProfileService
    {
        ResultDto<ExpertDetailsForProfileDto> Execute(long expertId);
    }
}
