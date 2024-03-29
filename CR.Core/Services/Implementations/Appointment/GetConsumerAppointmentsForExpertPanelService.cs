﻿using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Appointments;
using CR.Core.DTOs.ResultDTOs.Consumers;
using CR.Core.Services.Interfaces.Appointment;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
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
            var consumerAppointments = _context.Appointments
                .Include(a => a.ExpertInformation)
                .ThenInclude(e => e.Specialty)
                .Include(a => a.ConsumerInformation)
                .ThenInclude(a => a.Consumer)
                .Include(a => a.TimeOfDay)
                .ThenInclude(a => a.Day)
                .Where(a => a.ConsumerInformation.ConsumerId == consumerId && a.ExpertInformation.ExpertId == expertId && a.TimeOfDay.IsReserved)
                .OrderByDescending(a => a.TimeOfDay.Day.Date)
                .Select(a => new ConsumerAppointmentsForExpertPanelDto
                {
                    AppointmentDate = a.TimeOfDay.Day.Date_String,
                    AppointmentPrice = a.Price.ToString().GetPersianNumber(),
                    AppointmentTime = a.TimeOfDay.StartHour + " - " + a.TimeOfDay.FinishHour,
                    AppointmentReservationDate = a.CreateDate.ToShamsi(),
                    AppointmentStatus = a.AppointmentStatus.GetDisplayName(),
                    ExpertFullName = a.ExpertInformation.FirstName + " " + a.ExpertInformation.LastName,
                    ExpertIconSrc = a.ExpertInformation.IconSrc ?? "assets/img/icon-256x256.png",
                    ExpertSpeciality = a.ExpertInformation.Specialty.Name,
                }).AsEnumerable()
                .ToPaged(Page, PageSize, out var rowCount)
                .ToList();

            var consumer = _context.Appointments
                .Include(a => a.ConsumerInformation.ConsumerAppointments)
                .Include(a => a.ConsumerInformation)
                .ThenInclude(a => a.Consumer)
                .FirstOrDefault(a => a.ConsumerInformation.ConsumerId == consumerId);



            return new ResultDto<ResultGetConsumerAppointmentsForExpertPanel>()
            {
                Data = new ResultGetConsumerAppointmentsForExpertPanel()
                {
                    ConsumerAppointmentsForExpertPanelDtos = consumerAppointments,
                    ConsumerIconSrc = consumer.ConsumerInformation.IconSrc ?? "assets/img/icon-256x256.png",
                    ConsumerFullName = consumer.ConsumerInformation.FirstName + " " + consumer.ConsumerInformation.LastName,
                    Age = consumer.ConsumerInformation.BirthDate.GetAge().ToString().GetPersianNumber(),
                    Province = consumer.ConsumerInformation.Province,
                    City = consumer.ConsumerInformation.City,
                    Degree = consumer.ConsumerInformation.Degree,
                    Gender = consumer.ConsumerInformation.Gender.GetDisplayName(),
                    PhoneNumber = (consumer.ConsumerInformation.ConsumerAppointments.Any(_ => _.CallingType == CallingType.PhoneCall)) ? consumer.ConsumerInformation.Consumer.PhoneNumber : null,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount,
                },
                IsSuccess = true
            };

        }
    }
}
