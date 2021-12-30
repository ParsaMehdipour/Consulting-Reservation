﻿using System.Linq;
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
                    AppointmentStatus = "HardCode",
                    AppointmentTime = (a.TimeOfDay.StartDate.Hour.ToString().GetPersianNumber() + ":" +
                                       a.TimeOfDay.StartDate.Minute.ToString().GetPersianNumber()) +
                                      " - " + (a.TimeOfDay.FinishDate.Hour.ToString().GetPersianNumber() + ":" +
                                               a.TimeOfDay.FinishDate.Minute.ToString().GetPersianNumber()),
                    ExpertFullName = a.ExpertInformation.FirstName + " " + a.ExpertInformation.LastName,
                    ExpertIconSrc = a.ExpertInformation.IconSrc,
                    ExpertSpeciality = a.ExpertInformation.Specialty.Name,
                    Age = a.ConsumerInformation.BirthDate.GetAge().ToString().GetPersianNumber(),
                    Province = a.ConsumerInformation.Province,
                    City = a.ConsumerInformation.City,
                    Gender = a.ConsumerInformation.Gender.GetDisplayName(),
                    PhoneNumber = a.ConsumerInformation.Consumer.PhoneNumber.GetPersianNumber(),
                    BloodType = a.ConsumerInformation.BloodType
                }).OrderBy(a => a.AppointmentDate)
                .AsEnumerable()
                .ToPaged(Page, PageSize, out rowCount)
                .ToList();

            return new ResultDto<ResultGetConsumerAppointmentsForExpertPanel>()
            {
                Data = new ResultGetConsumerAppointmentsForExpertPanel()
                {
                    ConsumerAppointmentsForExpertPanelDtos = consumerAppointments,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount,
                },
                IsSuccess = true
            };

        }
    }
}
