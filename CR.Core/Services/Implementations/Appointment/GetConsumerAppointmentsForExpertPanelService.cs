using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Appointments;
using CR.Core.DTOs.ResultDTOs.Consumers;
using CR.Core.Services.Interfaces.Appointment;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CR.Core.Services.Implementations.Appointment
{
    public class GetConsumerAppointmentsForExpertPanelService : IGetConsumerAppointmentsForExpertPanelService
    {
        private readonly ApplicationContext _context;

        public GetConsumerAppointmentsForExpertPanelService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetConsumerAppointmentsForExpertPanel> Execute(long expertId, long consumerId, int Page = 1, int PageSize = 20)
        {

            int rowCount = 0;

            var consumerAppointments = _context.Appointments
                .Include(a => a.ExpertInformation)
                .ThenInclude(e => e.Specialty)
                .Include(a => a.ConsumerInformation)
                .ThenInclude(a => a.Consumer)
                .Include(a => a.TimeOfDay)
                .ThenInclude(a => a.Day)
                .Where(a => a.ConsumerInformation.ConsumerId == consumerId && a.ExpertInformation.ExpertId == expertId)
                .OrderByDescending(a => a.TimeOfDay.Day.Date)
                .Select(a => new ConsumerAppointmentsForExpertPanelDto
                {
                    AppointmentDate = a.TimeOfDay.Day.Date_String,
                    AppointmentPrice = a.Price.ToString().GetPersianNumber(),
                    AppointmentTime = a.TimeOfDay.StartHour + " - " + a.TimeOfDay.FinishHour,
                    AppointmentReservationDate = a.CreateDate.ToShamsi(),
                    AppointmentStatus = a.AppointmentStatus.GetDisplayName(),
                    ExpertFullName = a.ExpertInformation.FirstName + " " + a.ExpertInformation.LastName,
                    ExpertIconSrc = a.ExpertInformation.IconSrc,
                    ExpertSpeciality = a.ExpertInformation.Specialty.Name,
                }).AsEnumerable()
                .ToPaged(Page, PageSize, out rowCount)
                .ToList();

            var consumer = _context.Appointments
                .Include(a => a.ConsumerInformation)
                .ThenInclude(a => a.Consumer).FirstOrDefault(a => a.ConsumerInformation.ConsumerId == consumerId);



            return new ResultDto<ResultGetConsumerAppointmentsForExpertPanel>()
            {
                Data = new ResultGetConsumerAppointmentsForExpertPanel()
                {
                    ConsumerAppointmentsForExpertPanelDtos = consumerAppointments,
                    ConsumerIconSrc = consumer.ConsumerInformation.IconSrc,
                    Age = consumer.ConsumerInformation.BirthDate.GetAge().ToString().GetPersianNumber(),
                    Province = consumer.ConsumerInformation.Province,
                    City = consumer.ConsumerInformation.City,
                    Degree = consumer.ConsumerInformation.Degree,
                    Gender = consumer.ConsumerInformation.Gender.GetDisplayName(),
                    PhoneNumber = consumer.ConsumerInformation.Consumer.PhoneNumber.GetPersianNumber(),
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount,
                },
                IsSuccess = true
            };

        }
    }
}
