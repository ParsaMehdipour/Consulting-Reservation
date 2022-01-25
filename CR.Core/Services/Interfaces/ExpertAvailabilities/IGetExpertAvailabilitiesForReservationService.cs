using System.Collections.Generic;
using CR.Common.DTOs;
using CR.Core.DTOs.ExpertAvailabilities;
using CR.DataAccess.Enums;

namespace CR.Core.Services.Interfaces.ExpertAvailabilities
{
    public interface IGetExpertAvailabilitiesForReservationService
    {
        ResultDto<List<DayDto>> Execute(long expertInformationId , TimingType timingType);
    }
}
