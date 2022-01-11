using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs;

namespace CR.Core.Services.Interfaces.Experts
{
    public interface IEditBasicExpertDetailsService
    {
        ResultDto Execute(RequestEditBasicExpertDetailsDto request);
    }
}
