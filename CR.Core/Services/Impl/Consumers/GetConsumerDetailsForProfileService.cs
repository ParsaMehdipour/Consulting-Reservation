using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Core.DTOs.Consumers;
using CR.Core.DTOs.ResultDTOs;
using CR.Core.Services.Interfaces.Consumers;
using CR.DataAccess.Context;
using System.Linq;
using CR.Common.Utilities;

namespace CR.Core.Services.Impl.Consumers
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
            var consumer = _context.Users.FirstOrDefault(p=>p.Id == consumerId);

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
                    bloodType = consumerInformation.BloodType,
                    province = consumerInformation.Province,
                    city = consumerInformation.City,
                    specificAddress = consumerInformation.SpecificAddress,
                    postalCode = consumerInformation.PostalCode.GetPersianNumber(),
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
                Message = "لطفااطلاعات خود را وارد کنید"
            };
        }
    }
}
