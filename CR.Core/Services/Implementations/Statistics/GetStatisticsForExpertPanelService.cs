using System;
using System.Linq;
using CR.Core.DTOs.Statistics;
using CR.Core.Services.Interfaces.Statistics;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace CR.Core.Services.Implementations.Statistics
{
    public class GetStatisticsForExpertPanelService : IGetStatisticsForExpertPanelService
    {
        private readonly ApplicationContext _context;

        public GetStatisticsForExpertPanelService(ApplicationContext context)
        {
            _context = context;
        }

        public StatisticsForExpertPanelDto Execute(long expertId)
        {
            var statisticsForExpertPanel = new StatisticsForExpertPanelDto
            {
                AllAppointmentCount = _context.Appointments.Include(e => e.ExpertInformation)
                    .Count(a => a.ExpertInformation.ExpertId == expertId && a.TimeOfDay.IsReserved == true),

                AllConsumersCount = _context.Appointments
                    .Include(a => a.ConsumerInformation)
                    .ThenInclude(a => a.Consumer)
                    .Include(a => a.ExpertInformation)
                    .Where(a => a.ExpertInformation.ExpertId == expertId && a.TimeOfDay.IsReserved == true)
                    .OrderByDescending(a => a.ConsumerInformation.ConsumerId).Distinct().Count(),

                TodayConsumersCount = _context.Appointments
                    .Include(a => a.TimeOfDay)
                    .ThenInclude(t => t.Day)
                    .Include(a => a.ConsumerInformation)
                    .ThenInclude(a => a.Consumer)
                    .Include(a => a.ExpertInformation)
                    .Where(a => a.ExpertInformation.ExpertId == expertId &&
                                a.TimeOfDay.Day.Date.Date == DateTime.Now.Date
                                && a.TimeOfDay.IsReserved == true)
                    .OrderByDescending(a => a.ConsumerInformation.ConsumerId).Distinct().Count()
            };

            return statisticsForExpertPanel;
        }
    }
}
