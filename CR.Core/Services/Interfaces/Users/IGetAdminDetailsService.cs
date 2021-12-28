using CR.Common.DTOs;
using CR.Core.DTOs.Users;

namespace CR.Core.Services.Interfaces.Users
{
    public interface IGetAdminDetailsService
    {
        ResultDto<AdminDetailsDto> Execute(long adminId);
    }
}
