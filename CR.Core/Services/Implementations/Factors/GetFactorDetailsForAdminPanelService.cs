using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Appointments;
using CR.Core.DTOs.Factors;
using CR.Core.Services.Interfaces.Factors;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CR.Core.Services.Implementations.Factors
{
    public class GetFactorDetailsForAdminPanelService : IGetFactorDetailsForAdminPanelService
    {
        private readonly ApplicationContext _context;

        public GetFactorDetailsForAdminPanelService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<FactorDetailsForAdminPanelDto> Execute(long factorId)
        {
            var factor = _context.Factors.Include(_ => _.ExpertInformation)
                .Include(_ => _.ConsumerInformation)
                .Include(_ => _.Appointments)
                .ThenInclude(_ => _.TimeOfDay)
                .ThenInclude(_ => _.Day).FirstOrDefault(_ => _.Id == factorId);

            if (factor == null)
            {
                return new ResultDto<FactorDetailsForAdminPanelDto>()
                {
                    IsSuccess = false,
                    Message = "فاکتور یافت نشد",
                    Data = new FactorDetailsForAdminPanelDto()
                };
            }

            var result = new FactorDetailsForAdminPanelDto()
            {
                AppointmentDetailsForSiteDtos = factor.Appointments.Select(_ => new AppointmentDetailsForSiteDto()
                {
                    CallingType = _.CallingType.GetDisplayName(),
                    Date = _.TimeOfDay.Day.Date_String,
                    Price = _.Price.Value,
                    Time = _.TimeOfDay.StartHour + " - " + _.TimeOfDay.FinishHour,
                    TimingType = _.TimeOfDay.TimingType.GetDisplayName()

                }).ToList(),
                consumerFullName = factor.ConsumerInformation.FirstName + " " + factor.ConsumerInformation.LastName,
                consumerIconSrc = factor.ConsumerInformation.IconSrc ?? "assets/img/User.png",
                consumerId = factor.ConsumerInformation.ConsumerId,
                expertFullName = factor.ExpertInformation.FirstName + " " + factor.ExpertInformation.LastName,
                expertIconSrc = factor.ExpertInformation.IconSrc ?? "assets/img/User.png",
                expertId = factor.ExpertInformation.ExpertId,
                price = factor.TotalPrice,
                FactorStatus = factor.FactorStatus.GetDisplayName(),
                FactorNumber = factor.FactorNumber,
                CommissionPrice = factor.Appointments.Sum(_ => _.CommissionPrice),
                DiscountPrice = factor.Appointments.Sum(_ => _.DiscountPrice),
                RawPrice = factor.Appointments.Sum(_ => _.RawPrice),
                CreateDate = factor.CreateDate.ToShamsi()
            };

            return new ResultDto<FactorDetailsForAdminPanelDto>()
            {
                IsSuccess = true,
                Message = string.Empty,
                Data = result
            };
        }
    }
}
