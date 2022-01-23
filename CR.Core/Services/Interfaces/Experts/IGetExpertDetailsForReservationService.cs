using CR.Common.DTOs;
using CR.Core.DTOs.Experts;
using CR.DataAccess.Enums;

namespace CR.Core.Services.Interfaces.Experts
{
    public interface IGetExpertDetailsForReservationService
    {
        ResultDto<ExpertDetailsForReservationDto> Execute(long expertInformationId);
    }
}
