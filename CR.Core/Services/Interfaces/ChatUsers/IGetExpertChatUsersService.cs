using CR.Common.DTOs;
using CR.Core.DTOs.ChatUsers;
using System.Collections.Generic;

namespace CR.Core.Services.Interfaces.ChatUsers
{
    public interface IGetExpertChatUsersService
    {
        ResultDto<List<ChatUserForExpertPanelDto>> Execute(long expertId, string searchKey);
    }
}
