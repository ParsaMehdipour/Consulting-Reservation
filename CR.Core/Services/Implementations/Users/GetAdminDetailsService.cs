using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Users;
using CR.Core.Services.Interfaces.Users;
using CR.DataAccess.Context;
using System.Linq;

namespace CR.Core.Services.Implementations.Users
{
    public class GetAdminDetailsService : IGetAdminDetailsService
    {
        private readonly ApplicationContext _context;

        public GetAdminDetailsService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<AdminDetailsDto> Execute(long adminId)
        {
            var admin = _context.Users
                .FirstOrDefault(u => u.Id == adminId);

            if (admin == null)
            {
                return new ResultDto<AdminDetailsDto>()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "مدیر یافت نشد"
                };
            }

            if (admin.ExpertInformationId != null)
            {
                var expertInformation = _context.ExpertInformations
                    .FirstOrDefault(e => e.ExpertId == adminId);

                if (expertInformation == null)
                {
                    return new ResultDto<AdminDetailsDto>()
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "اطلاعات مدیر یافت نشد"
                    };
                }

                var number = admin.PhoneNumber;
                return new ResultDto<AdminDetailsDto>()
                {
                    Data = new AdminDetailsDto()
                    {
                        adminId = admin.Id,
                        informationId = expertInformation.Id,
                        firstName = expertInformation.FirstName,
                        lastName = expertInformation.LastName,
                        birthDate = expertInformation.BirthDate_String,
                        province = expertInformation.Province,
                        city = expertInformation.City,
                        phoneNumber = admin.PhoneNumber.GetPersianNumber() ?? admin.UserName,
                        email = admin.Email,
                        specificAddress = expertInformation.SpecificAddress,
                        degree = expertInformation.PostalCode.GetPersianNumber(),
                        iconSrc = admin.IconSrc ?? "assets/img/User.png"
                    },
                    IsSuccess = true
                };
            }
            else
            {
                var consumerInformation = _context.ConsumerInfromations
                    .FirstOrDefault(e => e.ConsumerId == adminId);

                if (consumerInformation == null)
                {
                    return new ResultDto<AdminDetailsDto>()
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "اطلاعات مدیر یافت نشد"
                    };
                }


                var number = admin.PhoneNumber;
                return new ResultDto<AdminDetailsDto>()
                {
                    Data = new AdminDetailsDto()
                    {
                        adminId = admin.Id,
                        informationId = consumerInformation.Id,
                        firstName = consumerInformation.FirstName,
                        lastName = consumerInformation.LastName,
                        birthDate = consumerInformation.BirthDate_String,
                        province = consumerInformation.Province,
                        city = consumerInformation.City,
                        phoneNumber = admin.PhoneNumber ?? admin.UserName,
                        email = admin.Email,
                        specificAddress = consumerInformation.SpecificAddress,
                        degree = consumerInformation.Degree,
                        iconSrc = admin.IconSrc
                    },
                    IsSuccess = true
                };
            }

        }
    }
}
