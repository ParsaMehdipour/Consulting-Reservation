using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Consumers;
using CR.Core.Services.Interfaces.Consumers;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CR.Core.Services.Implementations.Consumers
{
    public class GetLatestConsumersForAdminService : IGetLatestConsumersForAdminService
    {
        private readonly ApplicationContext _context;

        public GetLatestConsumersForAdminService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<List<LatestConsumerForAdminDto>> Execute()
        {

            var latestConsumers = _context.Users
                .Include(u => u.ConsumerInfromation)
                .ThenInclude(u => u.ConsumerAppointments)
                .ThenInclude(u => u.TimeOfDay)
                .ThenInclude(u => u.Day)
                .Where(u => u.UserFlag == UserFlag.Consumer && u.ConsumerInfromation != null)
                .Select(u => new LatestConsumerForAdminDto
                {
                    id = u.Id,
                    firstName = u.ConsumerInfromation.FirstName,
                    lastName = u.ConsumerInfromation.LastName,
                    phoneNumber = u.PhoneNumber.GetPersianNumber(),
                    paidAmount = u.ConsumerInfromation.ConsumerAppointments.Where(_ => _.AppointmentStatus == AppointmentStatus.Completed || _.AppointmentStatus == AppointmentStatus.NotDone || _.AppointmentStatus == AppointmentStatus.Waiting).Sum(_ => _.Price).ToString(),
                    lastVisit = u.ConsumerInfromation.ConsumerAppointments.OrderBy(_ => _.Id).LastOrDefault(_ => _.AppointmentStatus == AppointmentStatus.Completed || _.AppointmentStatus == AppointmentStatus.NotDone).TimeOfDay.Day.Date_String ?? "بدون نوبت",
                    iconSrc = u.ConsumerInfromation.IconSrc ?? "assets/img/icon-256x256.png"
                }).Take(5).ToList();

            return new ResultDto<List<LatestConsumerForAdminDto>>()
            {
                Data = latestConsumers,
                IsSuccess = true
            };
        }
    }
}
