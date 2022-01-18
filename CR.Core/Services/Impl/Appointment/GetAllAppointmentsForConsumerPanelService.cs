﻿using System.Linq;
using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Appointments;
using CR.Core.DTOs.ResultDTOs;
using CR.Core.Services.Interfaces.Appointment;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace CR.Core.Services.Impl.Appointment
{
    public class GetAllAppointmentsForConsumerPanelService : IGetAllAppointmentsForConsumerPanelService
    {
        private readonly ApplicationContext _context;

        public GetAllAppointmentsForConsumerPanelService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetAllAppointmentsForConsumerPanelDto> Execute(long consumerId,int Page = 1, int PageSize = 20)
        {
            int rowCount = 0;
            var appointmentsForConsumerPanel = _context.Appointments
                .Include(a => a.ExpertInformation)
                .ThenInclude(e=>e.Specialty)
                .Include(a => a.ConsumerInformation)
                .Include(a => a.TimeOfDay)
                .ThenInclude(a => a.Day)
                .Where(a=>a.ConsumerInformation.ConsumerId == consumerId)
                .OrderByDescending(a => a.TimeOfDay.Day.Date)
                .Select(a => new AppointmentForConsumerPanelDto
                {
                    Id = a.Id,
                    AppointmentDate = a.TimeOfDay.Day.Date_String,
                    ExpertFullName = a.ExpertInformation.FirstName + " " + a.ExpertInformation.LastName,
                    ExpertInformationId = a.ExpertInformationId,
                    ExpertIconSrc = a.ExpertInformation.IconSrc ?? "assets/img/icon-256x256.png",
                    ExpertTracking = "HardCode",
                    Price = a.Price.ToString().GetPersianNumber(),
                    ReservationDate = a.CreateDate.ToShamsi(),
                    Status = a.AppointmentStatus.GetDisplayName(),
                    Speciality = a.ExpertInformation.Specialty.Name
                }).AsEnumerable()?
                .ToPaged(Page, PageSize, out rowCount).ToList();


            return new ResultDto<ResultGetAllAppointmentsForConsumerPanelDto>()
            {
                Data = new ResultGetAllAppointmentsForConsumerPanelDto()
                {
                    AppointmentForConsumerPanelDtos = appointmentsForConsumerPanel,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount,
                },
                IsSuccess = true
            };
        }
    }
}
