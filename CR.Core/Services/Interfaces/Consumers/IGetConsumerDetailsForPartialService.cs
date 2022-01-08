using CR.Common.DTOs;
using CR.Core.DTOs.Consumers;

namespace CR.Core.Services.Interfaces.Consumers
{
    public interface IGetConsumerDetailsForPartialService
    {
        ResultDto<ConsumerForPartialDto> Execute(long consumerId);
    }
}
