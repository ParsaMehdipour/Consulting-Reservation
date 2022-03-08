using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.Chat;

namespace CR.Core.Services.Interfaces.ChatMessages
{
    public interface IAddNewChatMessageService
    {
        ResultDto Execute(RequestAddNewChatMessageDto request);
    }
}
