using CR.Core.Services.Interfaces.Users;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using System.Linq;
using CR.Core.DTOs.ResultDTOs;

namespace CR.Core.Services.Impl.Users
{
    public class GetUserFlagService : IGetUserFlagService
    {
        private readonly ApplicationContext _context;

        public GetUserFlagService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultCheckUserFlagService Execute(long? userId)
        {
            var user = _context.Users.FirstOrDefault(u=>u.Id == userId);

            return new ResultCheckUserFlagService
            {
                IsActive = user.IsActive,
                UserFlag = user.UserFlag
            };
        }
    }
}
