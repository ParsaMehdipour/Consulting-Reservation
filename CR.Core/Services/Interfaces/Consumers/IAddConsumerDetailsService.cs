using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs;

namespace CR.Core.Services.Interfaces.Consumers
{
    public interface IAddConsumerDetailsService
    {
        ResultDto Execute(RequestAddConsumerDetailsDto request);
    }
}
