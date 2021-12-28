using CR.Common.DTOs;

namespace CR.Core.Services.Interfaces.ExpertAvailabilities
{
    public interface IRemoveTimeOfDayService
    {
        ResultDto Execute(long id);
    }
}
