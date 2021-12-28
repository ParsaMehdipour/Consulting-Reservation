using System.Linq;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Statistics;
using CR.Core.Services.Interfaces.Statistics;
using CR.DataAccess.Context;

namespace CR.Core.Services.Impl.Statistics
{
    public class GetStatisticsForAdminPanelService : IGetStatisticsForAdminPanelService
    {
        private readonly ApplicationContext _context;

        public GetStatisticsForAdminPanelService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<StatisticsForAdminPanelDto> Execute()
        {
            var consumerCount = _context.Users.Count(u => u.ConsumerInfromation != null).ToString().GetPersianNumber();
            var expertCount = _context.Users.Count(e => e.IsActive == true && e.ExpertInformation != null).ToString().GetPersianNumber();
            var appointmentCount = _context.Appointments.Count().ToString().GetPersianNumber();
            var income = 500.ToString().GetPersianNumber();

            return new ResultDto<StatisticsForAdminPanelDto>()
            {
                Data = new StatisticsForAdminPanelDto()
                {
                    AppointmentCount = appointmentCount,
                    ConsumerCount = consumerCount,
                    ExpertCount = expertCount,
                    Income = income
                },
                IsSuccess = true
            };
        }
    }
}
