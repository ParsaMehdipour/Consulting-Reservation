using CR.Common.DTOs;
using CR.Core.DTOs.ResultDTOs.ChatMessages;

namespace CR.Core.Services.Interfaces.ChatMessages
{
    public interface IGetChatMessagesService
    {
        ResultDto<ResultGetChatMessagesDto> Execute(long chatUserId);
    }
}
