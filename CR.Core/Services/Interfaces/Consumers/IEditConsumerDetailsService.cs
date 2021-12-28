using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs;

namespace CR.Core.Services.Interfaces.Consumers
{
    public interface IEditConsumerDetailsService
    {
        ResultDto Execute(RequestEditConsumerDetailsDto request);
    }
}
