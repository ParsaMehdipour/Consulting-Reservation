using CR.Common.DTOs;

namespace CR.Core.Services.Interfaces.Consumers
{
    public interface IChangeConsumerStatusService
    {
        ResultDto Execute(long consumerId, bool activeStatus);
    }
}