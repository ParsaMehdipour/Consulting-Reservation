using CR.Common.DTOs;
using CR.Core.DTOs.Images;
using CR.Core.DTOs.RequestDTOs.ChatUser;
using CR.Core.Services.Interfaces.ChatUsers;
using CR.Core.Services.Interfaces.Images;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CR.Core.Services.Implementations.ChatUsers
{
    public class CheckForAppointmentTimeService : ICheckForAppointmentTimeService
    {
        private readonly ApplicationContext _context;
        private readonly IImageUploaderService _imageUploaderService;

        public CheckForAppointmentTimeService(ApplicationContext context
        , IImageUploaderService imageUploaderService)
        {
            _context = context;
            _imageUploaderService = imageUploaderService;
        }

        public ResultDto<string> Execute(RequestCheckForAppointmentTimeDto request)
        {

            if (request.chatUserId == 0)
            {
                return new ResultDto<string>()
                {
                    IsSuccess = false,
                    Message = "لطفا یکی از مخاطبین را انتخاب نمایید",
                };
            }

            if (string.IsNullOrWhiteSpace(request.message) && request.file == null)
            {
                return new ResultDto<string>()
                {
                    IsSuccess = false,
                    Message = "لطفا مطلبی جهت ارسال وارد کنید",
                };
            }

            var chatUser = _context.ChatUsers
                .Include(_ => _.Appointment)
                .ThenInclude(_ => _.TimeOfDay)
                .FirstOrDefault(_ => _.Id == request.chatUserId);

            if (chatUser!.Appointment.IsClosed)
            {
                return new ResultDto<string>()
                {
                    IsSuccess = false,
                    Message = "نوبت شما شما توسط سیستم بسته شده است"
                };
            }

            if (DateTime.Now < chatUser?.Appointment.TimeOfDay.StartTime)
            {
                return new ResultDto<string>()
                {
                    IsSuccess = false,
                    Message = "زمان شروع نوبت شما نرسیده است"
                };
            }

            if (DateTime.Now > chatUser?.Appointment.TimeOfDay.FinishTime.AddMinutes(5))
            {
                return new ResultDto<string>()
                {
                    IsSuccess = false,
                    Message = "زمان نوبت شما پایان یافت"
                };
            }

            if (request.file != null)
            {
                var fileName = _imageUploaderService.Execute(new UploadImageDto()
                {
                    File = request.file,
                    Folder = "Chat"
                });

                return new ResultDto<string>()
                {
                    Data = fileName,
                    IsSuccess = true,
                    Message = string.Empty
                };
            }

            return new ResultDto<string>()
            {
                IsSuccess = true,
                Message = string.Empty
            };
        }
    }
}
