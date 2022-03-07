using CR.Common.DTOs;
using CR.Core.DTOs.ChatUsers;
using System.Collections.Generic;

namespace CR.Core.Services.Interfaces.ChatUsers
{
    public interface IGetConsumerChatUsersService
    {
        ResultDto<List<ChatUserForConsumerDto>> Execute(long consumerId, string searchKey);
    }
}
