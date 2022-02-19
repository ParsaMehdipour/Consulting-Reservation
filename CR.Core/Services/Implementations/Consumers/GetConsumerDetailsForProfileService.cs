using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Consumers;
using CR.Core.DTOs.ResultDTOs.Consumers;
using CR.Core.Services.Interfaces.Consumers;
using CR.DataAccess.Context;
using System.Linq;

namespace CR.Core.Services.Implementations.Consumers
{
    public class GetConsumerDetailsForProfileService : IGetConsumerDetailsForProfileService
    {
        private readonly ApplicationContext _context;

        public GetConsumerDetailsForProfileService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetConsumerDetailsForSiteDto> Execute(long consumerId)
        {
            var consumer = _context.Users.FirstOrDefault(p => p.Id == consumerId);

            var consumerInformationId = consumer.ConsumerInformationId;

            if (consumerInformationId != null)
            {
                var consumerInformation = _context.ConsumerInfromations.FirstOrDefault(ci => ci.Id == consumerInformationId);

                if (consumerInformation == null)
                {
                    return new ResultDto<ResultGetConsumerDetailsForSiteDto>()
                    {
                        IsSuccess = false,
                        Message = "اطلاعات شما یافت نشد!!"
                    };
                }

                var consumerDetails = new ConsumerDetailsForSiteDto
                {
                    id = consumerInformation.Id,
                    userName = consumer.UserName,
                    firstName = consumerInformation.FirstName,
                    lastName = consumerInformation.LastName,
                    iconSrc = consumerInformation.IconSrc ?? "assets/img/icon-256x256.png",
                    province = consumerInformation.Province,
                    city = consumerInformation.City,
                    specificAddress = consumerInformation.SpecificAddress,
                    degree = consumerInformation.Degree,
                    phoneNumber = consumer.PhoneNumber.GetPersianNumber(),
                    email = consumer.Email,
                    birthDate = consumerInformation.BirthDate,
                    gender = consumerInformation.Gender
                };

                return new ResultDto<ResultGetConsumerDetailsForSiteDto>
                {
                    Data = new ResultGetConsumerDetailsForSiteDto
                    {
                        ConsumerDetailsForSiteDto = consumerDetails
                    },
                    IsSuccess = true
                };

            }

            return new ResultDto<ResultGetConsumerDetailsForSiteDto>
            {
                Data = new ResultGetConsumerDetailsForSiteDto
                {
                    ConsumerDetailsForSiteDto = null
                },
                IsSuccess = false,
                Message = "لطفا اطلاعات خود را وارد کنید"
            };
        }
    }
}
