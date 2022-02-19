using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Consumers;
using CR.Core.Services.Interfaces.Consumers;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using System.Linq;

namespace CR.Core.Services.Implementations.Consumers
{
    public class GetConsumerDetailsForAdminService : IGetConsumerDetailsForAdminService
    {
        private readonly ApplicationContext _context;

        public GetConsumerDetailsForAdminService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ConsumerDetailsForAdminDto> Execute(long consumerId)
        {
            var consumer = _context.Users
                .Where(u => u.UserFlag == UserFlag.Consumer)
                .FirstOrDefault(u => u.Id == consumerId);

            if (consumer == null)
            {
                return new ResultDto<ConsumerDetailsForAdminDto>()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "متخصص یافت نشد"
                };
            }

            var consumerInformation = _context.ConsumerInfromations
                .FirstOrDefault(e => e.ConsumerId == consumerId);

            if (consumerInformation == null)
            {
                return new ResultDto<ConsumerDetailsForAdminDto>()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "اطلاعات متخصص یافت نشد"
                };
            }

            var number = consumer.PhoneNumber;
            return new ResultDto<ConsumerDetailsForAdminDto>()
            {
                Data = new ConsumerDetailsForAdminDto()
                {
                    consumerInformationId = consumerInformation.Id,
                    firstName = consumerInformation.FirstName,
                    lastName = consumerInformation.LastName,
                    birthDate = consumerInformation.BirthDate,
                    birthDate_String = consumerInformation.BirthDate_String,
                    province = consumerInformation.Province,
                    city = consumerInformation.City,
                    phoneNumber = consumer.PhoneNumber.GetPersianNumber() ?? consumer.UserName,
                    email = consumer.Email,
                    specificAddress = consumerInformation.SpecificAddress,
                    degree = consumerInformation.Degree.GetPersianNumber(),
                    iconSrc = consumerInformation.IconSrc ?? "assets/img/icon-256x256.png"
                },
                IsSuccess = true
            };
        }
    }
}
