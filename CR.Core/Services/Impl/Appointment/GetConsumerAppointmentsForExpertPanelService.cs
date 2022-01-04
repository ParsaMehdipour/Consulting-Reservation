using System.Linq;
using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Appointment;
using CR.Core.DTOs.ResultDTOs;
using CR.Core.Services.Interfaces.Appointment;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace CR.Core.Services.Impl.Appointment
{
    public class GetConsumerAppointmentsForExpertPanelService : IGetConsumerAppointmentsForExpertPanelService
    {
        private readonly ApplicationContext _context;

        public GetConsumerAppointmentsForExpertPanelService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetConsumerAppointmentsForExpertPanel> Execute(long expertId,long consumerId,int Page = 1, int PageSize = 20)
        {

            int rowCount = 0;

            var consumerAppointments = _context.Appointments
                .Include(a => a.ExpertInformation)
                .ThenInclude(e => e.Specialty)
                .Include(a => a.ConsumerInformation)
                .ThenInclude(a=>a.Consumer)
                .Include(a => a.TimeOfDay)
                .ThenInclude(a => a.Day)
                .Where(a => a.ConsumerInformation.ConsumerId == consumerId && a.ExpertInformation.ExpertId == expertId)
                .Select(a => new ConsumerAppointmentsForExpertPanelDto
                {
                    AppointmentDate = a.TimeOfDay.Day.Date_String,
                    AppointmentPrice = a.TimeOfDay.Price.ToString().GetPersianNumber(),
                    AppointmentReservationDate = a.CreateDate.ToShamsi(),
                    AppointmentStatus = a.AppointmentStatus.GetDisplayName(),
                    AppointmentTime = (a.TimeOfDay.StartDate.Hour.ToString().GetPersianNumber() + ":" +
                                       a.TimeOfDay.StartDate.Minute.ToString().GetPersianNumber()) +
                                      " - " + (a.TimeOfDay.FinishDate.Hour.ToString().GetPersianNumber() + ":" +
                                               a.TimeOfDay.FinishDate.Minute.ToString().GetPersianNumber()),
                    ExpertFullName = a.ExpertInformation.FirstName + " " + a.ExpertInformation.LastName,
                    ExpertIconSrc = a.ExpertInformation.IconSrc,
                    ExpertSpeciality = a.ExpertInformation.Specialty.Name,
                }).OrderBy(a => a.AppointmentDate)
                .AsEnumerable()
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
                    Gender = consumer.ConsumerInformation.Gender.GetDisplayName(),
                    PhoneNumber = consumer.ConsumerInformation.Consumer.PhoneNumber.GetPersianNumber(),
                    BloodType = consumer.ConsumerInformation.BloodType,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount,
                },
                IsSuccess = true
            };

        }
    }
}
