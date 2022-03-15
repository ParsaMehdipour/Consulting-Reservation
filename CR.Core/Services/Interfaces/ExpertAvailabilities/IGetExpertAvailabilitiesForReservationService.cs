using CR.Common.DTOs;
using CR.Core.DTOs.ResultDTOs.ExpertAvailabilities;
using CR.DataAccess.Enums;
using System;

namespace CR.Core.Services.Interfaces.ExpertAvailabilities
{
    public interface IGetExpertAvailabilitiesForReservationService
    {
        ResultDto<ResultGetExpertAvailabilitiesDetailsDto> Execute(long expertInformationId, TimingType timingType, DateTime date);
    }
}
