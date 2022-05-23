using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Statistics;
using CR.Core.Services.Interfaces.Statistics;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CR.Core.Services.Implementations.Statistics
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
            var consumerCount = _context.Users.Count(u => u.UserFlag == UserFlag.Consumer).ToString().GetPersianNumber();
            var expertCount = _context.Users.Count(e => e.IsActive == true && e.UserFlag == UserFlag.Expert).ToString().GetPersianNumber();
            var appointmentCount = _context.Appointments.Count(_ => _.AppointmentStatus == AppointmentStatus.Completed).ToString().GetPersianNumber();
            var income = _context.Appointments
                .Where(a => a.AppointmentStatus == AppointmentStatus.Completed || a.AppointmentStatus == AppointmentStatus.NotDone || a.AppointmentStatus == AppointmentStatus.Waiting)
                .Sum(a => a.Price.Value).ToString("n0");


            StatisticsCountForAdminPanelDto consumerStatisticsCountForAdminPanelDto = GetConsumersYearStatistics();
            StatisticsCountForAdminPanelDto expertStatisticsCountForAdminPanelDto = GetExpertsYearStatistics();
            StatisticsAmountForAdminPanelDto incomeStatisticsForAdminPanelDto = GetIncomeStatisticsForAdminPanelDto();

            return new ResultDto<StatisticsForAdminPanelDto>()
            {
                Data = new StatisticsForAdminPanelDto()
                {
                    AppointmentCount = appointmentCount,
                    ConsumerCount = consumerCount,
                    ExpertCount = expertCount,
                    ConsumerStatisticsCountForAdminPanelDto = consumerStatisticsCountForAdminPanelDto,
                    ExpertStatisticsCountForAdminPanelDto = expertStatisticsCountForAdminPanelDto,
                    IncomeStatisticsForAdminPanelDto = incomeStatisticsForAdminPanelDto,
                    Income = income
                },
                IsSuccess = true
            };
        }

        private StatisticsCountForAdminPanelDto GetConsumersYearStatistics()
        {
            DateTime yearStart = DateTime.Now.Date.AddYears(-4);
            DateTime yearEnd = DateTime.Now.Date.AddYears(1);

            var consumerYearStatisticView = _context.Users
                .Where(u => u.UserFlag == UserFlag.Consumer && u.ConsumerInformationId != null)
                .AsQueryable()
                .Where(a => a.CreationDate.Year >= yearStart.Year && a.CreationDate.Year < yearEnd.Year)
                .OrderByDescending(a => a.CreationDate.Year)
                .Select(a => new { a.CreationDate.Year })
                .ToList();


            StatisticsCountForAdminPanelDto statisticPerYear = new StatisticsCountForAdminPanelDto() { Display = new string[5], Value = new int[5] };

            for (int i = 0; i <= 4; i++)
            {
                var currentYear = DateTime.Now.AddYears((4 - i) * (-1));
                if (i == 4)
                {
                    statisticPerYear.Display[i] = DateTime.Now.ToShamsiYear();
                }
                else
                {
                    statisticPerYear.Display[i] = DateTime.Now.AddYears(-(4 - i)).ToShamsiYear();
                }

                statisticPerYear.Value[i] = consumerYearStatisticView.Count(a => a.Year == currentYear.Year);
            }

            return statisticPerYear;
        }

        private StatisticsCountForAdminPanelDto GetExpertsYearStatistics()
        {
            DateTime yearStart = DateTime.Now.Date.AddYears(-4);
            DateTime yearEnd = DateTime.Now.Date.AddYears(1);

            var expertYearStatisticView = _context.Users
                .Where(u => u.UserFlag == UserFlag.Expert)
                .AsQueryable()
                .Where(a => a.CreationDate.Year >= yearStart.Year && a.CreationDate.Year < yearEnd.Year)
                .OrderByDescending(a => a.CreationDate.Year)
                .Select(a => new { a.CreationDate.Year })
                .ToList();

            StatisticsCountForAdminPanelDto statisticPerYear = new StatisticsCountForAdminPanelDto() { Display = new string[5], Value = new int[5] };

            for (int i = 0; i <= 4; i++)
            {
                var currentYear = DateTime.Now.AddYears((4 - i) * (-1));
                if (i == 4)
                {
                    statisticPerYear.Display[i] = "امسال";
                }
                else
                {
                    statisticPerYear.Display[i] = DateTime.Now.AddYears(-(4 - i)).ToShamsiYear();
                }

                statisticPerYear.Value[i] = expertYearStatisticView.Count(a => a.Year == currentYear.Year);
            }

            return statisticPerYear;
        }

        private StatisticsAmountForAdminPanelDto GetIncomeStatisticsForAdminPanelDto()
        {
            DateTime yearStart = DateTime.Now.Date.AddYears(-4);
            DateTime yearEnd = DateTime.Now.Date.AddYears(1);

            var incomeYearStatisticView = _context.Appointments
                .Include(_ => _.TimeOfDay)
                .ThenInclude(_ => _.Day)
                .Where(_ => _.AppointmentStatus == AppointmentStatus.Completed || _.AppointmentStatus == AppointmentStatus.NotDone || _.AppointmentStatus == AppointmentStatus.Waiting)
                .AsQueryable()
                .Where(a => a.TimeOfDay.Day.Date.Year >= yearStart.Year && a.TimeOfDay.Day.Date.Year < yearEnd.Year)
                .OrderByDescending(a => a.TimeOfDay.Day.Date.Year)
                .Select(a => new { a.Price, a.TimeOfDay.Day.Date.Year })
                .ToList();

            StatisticsAmountForAdminPanelDto statisticPerYear = new StatisticsAmountForAdminPanelDto() { Display = new string[5], Value = new long[5] };

            for (int i = 0; i <= 4; i++)
            {
                var currentYear = DateTime.Now.AddYears((4 - i) * (-1));
                if (i == 4)
                {
                    statisticPerYear.Display[i] = DateTime.Now.ToShamsiYear();
                }
                else
                {
                    statisticPerYear.Display[i] = DateTime.Now.AddYears(-(4 - i)).ToShamsiYear();
                }

                statisticPerYear.Value[i] = incomeYearStatisticView.Where(a => a.Year == currentYear.Year).Sum(a => a.Price.Value);
            }

            return statisticPerYear;
        }
    }
}
