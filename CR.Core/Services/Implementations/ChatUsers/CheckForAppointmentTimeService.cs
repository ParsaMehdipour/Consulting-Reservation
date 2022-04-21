using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.ChatUser;
using CR.Core.Services.Interfaces.ChatUsers;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CR.Core.Services.Implementations.ChatUsers
{
    public class CheckForAppointmentTimeService : ICheckForAppointmentTimeService
    {
        private readonly ApplicationContext _context;

        public CheckForAppointmentTimeService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestCheckForAppointmentTimeDto request)
        {

            if (string.IsNullOrWhiteSpace(request.message) && request.file == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "لطفا مطلبی جهت ارسال وارد کنید",
                };
            }

            var chatUser = _context.ChatUsers
                .Include(_ => _.Appointment)
                .ThenInclude(_ => _.TimeOfDay)
                .FirstOrDefault(_ => _.Id == request.chatUserId);

            if (DateTime.Now < chatUser?.Appointment.TimeOfDay.StartTime)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "زمان شروع نوبت شما نرسیده است"
                };
            }

            if (DateTime.Now > chatUser?.Appointment.TimeOfDay.FinishTime)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "زمان نوبت شما پایان یافت"
                };
            }


            return new ResultDto()
            {
                IsSuccess = true,
                Message = string.Empty
            };
        }
    }
}
