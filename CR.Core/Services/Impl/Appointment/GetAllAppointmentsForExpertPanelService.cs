using System.Linq;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Appointment;
using CR.Core.DTOs.ResultDTOs;
using CR.Core.Services.Interfaces.Appointment;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace CR.Core.Services.Impl.Appointment
{
    public class GetAllAppointmentsForExpertPanelService : IGetAllAppointmentsForExpertPanelService
    {
        private readonly ApplicationContext _context;

        public GetAllAppointmentsForExpertPanelService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetAllAppointmentsForExpertPanelDto> Execute(long expertId,int Page = 1, int PageSize = 20)
        {
            int rowCount = 0;

            var appointmentsForExpertPanel = _context.Appointments
                .Include(a => a.ExpertInformation)
                .ThenInclude(e => e.Specialty)
                .Include(a => a.ConsumerInformation)
                .ThenInclude(a => a.Consumer)
                .Include(a => a.TimeOfDay)
                .ThenInclude(a => a.Day)
                .Where(a => a.ExpertInformation.ExpertId == expertId)
                .Select(a => new AppointmentForExpertPanelDto
                {
                    AppointmentDate = a.TimeOfDay.Day.Date_String,
                    AppointmentStatus = "HardCode",
                    AppointmentTime =
                        (a.TimeOfDay.StartDate.Hour.ToString().GetPersianNumber() + ":" +
                         a.TimeOfDay.StartDate.Minute.ToString().GetPersianNumber()).ToString() +
                        " - " + (a.TimeOfDay.FinishDate.Hour.ToString().GetPersianNumber() + ":" +
                                 a.TimeOfDay.FinishDate.Minute.ToString().GetPersianNumber()),
                    City = a.ConsumerInformation.City,
                    Province = a.ConsumerInformation.Province,
                    ConsumerFullName = a.ConsumerInformation.FirstName + " " + a.ConsumerInformation.LastName,
                    ConsumerIconSrc = a.ConsumerInformation.IconSrc ?? "assets/img/icon-256x256.png",
                    ConsumerId = a.ConsumerInformation.ConsumerId,
                    Email = a.ConsumerInformation.Consumer.Email,
                    PhoneNumber = a.ConsumerInformation.Consumer.PhoneNumber.ToString().GetPersianNumber()
                }).OrderBy(a => a.AppointmentDate)
                .AsEnumerable()
                .ToPaged(Page,PageSize,out rowCount)
                .ToList();

            return new ResultDto<ResultGetAllAppointmentsForExpertPanelDto>()
            {
                Data = new ResultGetAllAppointmentsForExpertPanelDto()
                {
                    AppointmentForExpertPanelDtos = appointmentsForExpertPanel,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount,
                },
                IsSuccess = true
            };
        }
    }
}
