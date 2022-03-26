using CR.Common.DTOs;
using CR.Core.DTOs.Users;

namespace CR.Core.Services.Interfaces.Users
{
    public interface IGetAdminDetailsForPartialService
    {
        ResultDto<AdminForPartialDto> Execute(long userId);
    }
}
