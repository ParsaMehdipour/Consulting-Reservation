using CR.Common.DTOs;
using CR.DataAccess.Enums;

namespace CR.Core.Services.Interfaces.ExpertAvailabilities
{
    public interface IGetTimeOfDayPriceForReservationService
    {
        ResultDto<long> Execute(CallingType callingType, long timeOfDayId);
    }
}
