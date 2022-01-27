using CR.Common.DTOs;
using CR.Core.DTOs.ExpertAvailabilities;

namespace CR.Core.Services.Interfaces.Experts
{
    public interface IGetExpertCallUsesService
    {
        ResultDto<ExpertCallUsesDto> Execute(long expertInformationId);
    }
}
