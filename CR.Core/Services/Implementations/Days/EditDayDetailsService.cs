using CR.Common.DTOs;
using CR.Core.DTOs.Days;
using CR.Core.Services.Interfaces.Days;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.ExpertAvailabilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CR.Core.Services.Implementations.Days
{
    public class EditDayDetailsService : IEditDayDetailsService
    {
        private readonly ApplicationContext _context;

        public EditDayDetailsService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestEditDayDetaislDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var day = _context.Days
                    .Include(_ => _.TimeOfDays)
                    .FirstOrDefault(_ => _.Id == request.dayId && _.ExpertInformationId == request.expertInformationId);

                if (day == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "روز یافت نشد!!"
                    };
                }



                var expertInformation =
                    _context.ExpertInformations.FirstOrDefault(e => e.Id == request.expertInformationId);

                if (expertInformation == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "اطلاعات مشاور یافت نشد!!"
                    };
                }

                _context.TimeOfDays.RemoveRange(day.TimeOfDays);

                if (request.timings != null)
                {

                    var timeOfDaysList = new List<TimeOfDay>();


                    foreach (var timingId in request.timings)
                    {
                        var timing = _context.Timings.FirstOrDefault(t => t.Id == timingId);

                        if (timing == null)
                        {
                            return new ResultDto()
                            {
                                IsSuccess = false,
                                Message = "زمانبندی یافت نشد!!"
                            };
                        }

                        var startMinutes = (timing.StartTime.Hour * 60) + timing.StartTime.Minute;
                        var endMinutes = (timing.EndTime.Hour * 60) + timing.EndTime.Minute;
                        var pureMinutes = endMinutes - startMinutes;

                        var timeOfDay = new TimeOfDay
                        {
                            DayId = request.dayId,
                            Day = day,
                            ExpertInformation = expertInformation,
                            ExpertInformationId = request.expertInformationId,
                            StartHour = timing.StartTime_String,
                            FinishHour = timing.EndTime_String,
                            StartTime = day.Date.AddHours(timing.StartTime.Hour).AddMinutes(timing.StartTime.Minute),
                            FinishTime = day.Date.AddHours(timing.EndTime.Hour).AddMinutes(timing.EndTime.Minute),
                            TimingType = timing.TimingType,
                            PhoneCallPrice = (pureMinutes * expertInformation.PhoneCallPrice),
                            VoiceCallPrice = (pureMinutes * expertInformation.VoiceCallPrice),
                            TextCallPrice = (pureMinutes * expertInformation.TextCallPrice),
                        };

                        timeOfDaysList.Add(timeOfDay);
                    }

                    var list = timeOfDaysList;

                    if (list.Count != list.GroupBy(_ => _.StartTime).Distinct().Count())
                    {
                        return new ResultDto()
                        {
                            IsSuccess = false,
                            Message = "در زمان بندی ها تداخل وجود دارد"
                        };
                    }

                    if (list.Count != list.GroupBy(_ => _.FinishTime).Distinct().Count())
                    {
                        return new ResultDto()
                        {
                            IsSuccess = false,
                            Message = "در زمان بندی ها تداخل وجود دارد"
                        };
                    }

                    _context.TimeOfDays.AddRange(timeOfDaysList);

                    _context.SaveChanges();

                    transaction.Commit();

                    return new ResultDto()
                    {
                        IsSuccess = true,
                        Message = "زمانبندی ها با موفقیت ویرایش شدند"
                    };
                }

                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "لطفا حداقل یک زمانبندی انتخاب کنید"
                };
            }
            catch (Exception)
            {
                transaction.Rollback();

                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "خطا!!"
                };
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}
