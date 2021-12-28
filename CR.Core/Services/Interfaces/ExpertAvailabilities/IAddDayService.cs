using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs;

namespace CR.Core.Services.Interfaces.ExpertAvailabilities
{
    public interface IAddDayService
    {
        ResultDto Execute(RequestAddNewDayDto request);
    }
}
