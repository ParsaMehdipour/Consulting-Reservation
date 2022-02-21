using System.Linq;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Experts;
using CR.Core.Services.Interfaces.Experts;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;

namespace CR.Core.Services.Implementations.Experts
{
    public class GetExpertDetailsForAdminService : IGetExpertDetailsForAdminService
    {
        private readonly ApplicationContext _context;

        public GetExpertDetailsForAdminService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ExpertDetailsForAdminDto> Execute(long expertId)
        {
            var expert = _context.Users
                .Where(u=>u.UserFlag == UserFlag.Expert)
                .FirstOrDefault(u => u.Id == expertId);

            if (expert == null)
            {
                return new ResultDto<ExpertDetailsForAdminDto>()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "مشاور یافت نشد"
                };
            }

            var expertInformation = _context.ExpertInformations
                .FirstOrDefault(e => e.ExpertId == expertId);

            if (expertInformation == null)
            {
                return new ResultDto<ExpertDetailsForAdminDto>()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "اطلاعات مشاور یافت نشد"
                };
            }

            var number = expert.PhoneNumber;
            return new ResultDto<ExpertDetailsForAdminDto>()
            {
                Data = new ExpertDetailsForAdminDto()
                {
                    expertInformationId = expertInformation.Id,
                    iconSrc = expertInformation.IconSrc ?? "assets/img/icon-256x256.png",
                    firstName = expertInformation.FirstName,
                    lastName = expertInformation.LastName,
                    birthDate = expertInformation.BirthDate,
                    birthDate_String = expertInformation.BirthDate_String,
                    province = expertInformation.Province,
                    city = expertInformation.City,
                    phoneNumber = expert.PhoneNumber.GetPersianNumber() ?? expert.UserName,
                    email = expert.Email,
                    bio = expertInformation.Bio,
                    specificAddress = expertInformation.SpecificAddress,
                    postalCode = expertInformation.PostalCode.GetPersianNumber()
                },
                IsSuccess = true
            };

        }
    }
}
