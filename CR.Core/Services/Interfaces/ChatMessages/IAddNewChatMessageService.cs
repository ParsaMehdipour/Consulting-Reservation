using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.Chat;
using CR.Core.DTOs.ResultDTOs.ChatMessages;

namespace CR.Core.Services.Interfaces.ChatMessages
{
    public interface IAddNewChatMessageService
    {
        ResultDto<ResultAddChatMessageDto> Execute(RequestAddNewChatMessageDto request);
    }
}
