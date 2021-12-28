using CR.Common.DTOs;
using CR.Core.DTOs.Experts;
using CR.Core.DTOs.RequestDTOs;

namespace CR.Core.Services.Interfaces.Experts
{
    public interface IEditExpertDetailsService
    {
        ResultDto Execute(ExpertDetailsForProfileDto request);
    }
}
