using CR.Common.DTOs;
using CR.Core.DTOs.Users;
using CR.Core.Services.Interfaces.Users;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using System.Linq;

namespace CR.Core.Services.Implementations.Users
{
    public class GetAdminDetailsForPartialService : IGetAdminDetailsForPartialService
    {
        private readonly ApplicationContext _context;

        public GetAdminDetailsForPartialService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<AdminForPartialDto> Execute(long userId)
        {
            var admin = _context.Users
                .Where(u => u.UserFlag == UserFlag.Admin)
                .FirstOrDefault(c => c.Id == userId);

            if (admin == null)
            {
                return new ResultDto<AdminForPartialDto>()
                {
                    IsSuccess = false,
                    Message = "اطلاعات شما یافت نشد"
                };
            }

            return new ResultDto<AdminForPartialDto>()
            {
                Data = new AdminForPartialDto()
                {
                    fullName = admin.FirstName + " " + admin.LastName,
                    iconSrc = admin.IconSrc ?? "assets/img/User.png"
                },
                IsSuccess = true
            };
        }
    }
}
