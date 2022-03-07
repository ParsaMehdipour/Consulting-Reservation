using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.ChatUser;

namespace CR.Core.Services.Interfaces.ChatUsers
{
    public interface IAddNewChatUserService
    {
        ResultDto Execute(RequestAddNewChatUserDto request);
    }
}
