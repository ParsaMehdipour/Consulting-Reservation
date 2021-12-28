using System.Threading.Tasks;
using CR.Common.DTOs;
using CR.Core.DTOs.Users;

namespace CR.Core.Services.Interfaces.Users
{
    public interface IRegisterExpertFromAdminService
    {
        Task<ResultDto> Execute(RegisterExpertFromAdminDto request);
    }
}
