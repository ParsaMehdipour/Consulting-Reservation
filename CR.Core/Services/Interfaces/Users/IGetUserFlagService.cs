using CR.Core.DTOs.ResultDTOs;
using CR.DataAccess.Enums;

namespace CR.Core.Services.Interfaces.Users
{
    public interface IGetUserFlagService
    {
        ResultCheckUserFlagService Execute(long? userId);
    }
}
