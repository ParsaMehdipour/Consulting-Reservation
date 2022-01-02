using System;
using CR.Common.DTOs;
using CR.Core.DTOs.Experts;

namespace CR.Core.Services.Interfaces.Experts
{
    public interface IGetThisDateExpertDetailsForReservationService
    {
        ResultDto<ExpertDetailsForReservationDto> Execute(long expertInformationId, DateTime date);
    }
}
