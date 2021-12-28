using CR.Common.DTOs;
using CR.Core.DTOs.Users;

namespace CR.Core.Services.Interfaces.Users
{
    public interface IEditAdminDetailsService
    {
        ResultDto Execute(AdminDetailsDto request);
    }
}
