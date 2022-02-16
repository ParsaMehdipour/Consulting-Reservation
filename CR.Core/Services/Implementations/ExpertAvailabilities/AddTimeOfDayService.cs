using System;
using System.Collections.Generic;
using System.Linq;
using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs;
using CR.Core.Services.Interfaces.ExpertAvailabilities;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.ExpertAvailabilities;

namespace CR.Core.Services.Implementations.ExpertAvailabilities
{
    public class AddTimeOfDayService : IAddTimeOfDayService
    {
        private readonly ApplicationContext _context;

        public AddTimeOfDayService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestAddNewTimeOfDayDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var day = _context.Days.FirstOrDefault(d => d.Id == request.dayId);

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
                        Message = "اطلاعات متخصص یافت نشد!!"
                    };
                }

                var timeOfDaysList = new List<TimeOfDay>();


                //foreach (var timeOfDayRequest in request.TimeOfDays)
                //{
                //    var timeOfDay = new TimeOfDay()
                //    {
                //        DayId = request.dayId,
                //        Day = day,
                //        ExpertInformation = expertInformation,
                //        ExpertInformationId = request.expertInformationId,
                //        Start = timeOfDayRequest.start,
                //        Finish = timeOfDayRequest.finish
                //    };

                //    timeOfDaysList.Add(timeOfDay);
                //}


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
                        StartTime = timing.StartTime,
                        FinishTime = timing.EndTime,
                        TimingType = timing.TimingType,
                        PhoneCallPrice = (pureMinutes * expertInformation.PhoneCallPrice) / 60,
                        VoiceCallPrice = (pureMinutes * expertInformation.VoiceCallPrice) / 60,
                        TextCallPrice = (pureMinutes * expertInformation.TextCallPrice) / 60,
                    };

                    timeOfDaysList.Add(timeOfDay);

                }

                var list = timeOfDaysList;

                _context.TimeOfDays.AddRange(timeOfDaysList);

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "زمانبندی ها با موفقیت افزوده شدند"
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                transaction.Rollback();

                throw;
            }

            finally
            {
                transaction.Dispose();
            }
        }
    }
}
