using System.Linq;
using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Consumers;
using CR.Core.Services.Interfaces.Consumers;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;

namespace CR.Core.Services.Implementations.Consumers
{
    public class GetConsumerDetailsForPartialService : IGetConsumerDetailsForPartialService
    {
        private readonly ApplicationContext _context;

        public GetConsumerDetailsForPartialService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ConsumerForPartialDto> Execute(long consumerId)
        {
            var consumer = _context.Users
                .Where(u => u.UserFlag == UserFlag.Consumer)
                .Include(u => u.ConsumerInfromation)
                .FirstOrDefault(c => c.Id == consumerId);

            if (consumer == null)
            {
                return new ResultDto<ConsumerForPartialDto>()
                {
                    IsSuccess = false,
                    Message = "اطلاعات شما یافت نشد"
                };
            }

            if (consumer.ConsumerInfromation == null)
            {
                return new ResultDto<ConsumerForPartialDto>()
                {
                    Data = new ConsumerForPartialDto()
                    {
                        age = "سن",
                        birthDate = "تاریخ تولد",
                        city = "شهر",
                        province = "استان",
                        fullName = "نام و نام خانوادگی شما",
                        iconSrc = "assets/img/User.png"
                    },
                    IsSuccess = true
                };
            }

            return new ResultDto<ConsumerForPartialDto>()
            {
                Data = new ConsumerForPartialDto()
                {
                    age = consumer.ConsumerInfromation.BirthDate.GetAge().ToString().GetPersianNumber(),
                    birthDate = consumer.ConsumerInfromation.BirthDate_String,
                    city = consumer.ConsumerInfromation.City,
                    province = consumer.ConsumerInfromation.Province,
                    fullName = consumer.ConsumerInfromation.FirstName + " " + consumer.ConsumerInfromation.LastName,
                    iconSrc = consumer.ConsumerInfromation.IconSrc ?? "assets/img/User.png"
                },
                IsSuccess = true
            };
        }
    }
}
