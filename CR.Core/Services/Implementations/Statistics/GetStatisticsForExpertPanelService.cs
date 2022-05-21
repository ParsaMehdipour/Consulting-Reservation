using CR.Core.DTOs.Statistics;
using CR.Core.Services.Interfaces.Statistics;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

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
                AllAppointmentCount = _context.Appointments
                    .Include(e => e.ExpertInformation)
                    .Include(_ => _.TimeOfDay)
                    .Count(a => a.ExpertInformation.ExpertId == expertId && a.TimeOfDay.IsReserved == true && a.TimeOfDay.StartTime.Date <= DateTime.Now.Date),

                AllConsumersCount = _context.Appointments
                    .Include(a => a.ExpertInformation)
                    .Include(a => a.TimeOfDay)
                    .Where(a => a.ExpertInformation.ExpertId == expertId && a.TimeOfDay.IsReserved == true)
                    .OrderByDescending(a => a.ConsumerInformationId).ToList().GroupBy(_ => _.ConsumerInformationId).Distinct().Count(),

                TodayConsumersCount = _context.Appointments
                    .Include(a => a.TimeOfDay)
                    .ThenInclude(t => t.Day)
                    .Include(a => a.ConsumerInformation)
                    .ThenInclude(a => a.Consumer)
                    .Include(a => a.ExpertInformation)
                    .Where(a => a.ExpertInformation.ExpertId == expertId && a.TimeOfDay.Day.Date.Date == DateTime.Now.Date && a.TimeOfDay.IsReserved == true)
                    .OrderByDescending(a => a.ConsumerInformationId)
                    .ToList().GroupBy(_ => _.ConsumerInformationId).Distinct().Count()

            };

            return statisticsForExpertPanel;
        }
    }
}
