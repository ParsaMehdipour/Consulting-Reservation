using System;
using System.Collections.Generic;
using System.Linq;
using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs;
using CR.Core.Services.Interfaces.ExpertAvailabilities;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.ExpertAvailabilities;

namespace CR.Core.Services.Impl.ExpertAvailabilities
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
                List<string> startHours = request.start[0].Split(',').ToList();
                List<string> finishHours = request.finish[0].Split(',').ToList();
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


                for (int i = 0; i < startHours.Count; i++)
                {
                    var timeOfDay = new TimeOfDay
                    {
                        DayId = request.dayId,
                        Day = day,
                        ExpertInformation = expertInformation,
                        ExpertInformationId = request.expertInformationId
                    };
                    timeOfDay.StartDate = day.Date;
                    timeOfDay.FinishDate = day.Date;
                    timeOfDay.StartDate = timeOfDay.StartDate.AddHours(Convert.ToInt32(startHours[i].Split(":")[0]));
                    timeOfDay.FinishDate = timeOfDay.FinishDate.AddHours(Convert.ToInt32(finishHours[i].Split(":")[0]));
                    timeOfDay.StartDate = timeOfDay.StartDate.AddMinutes(Convert.ToInt32(startHours[i].Split(":")[1]));
                    timeOfDay.FinishDate = timeOfDay.FinishDate.AddMinutes(Convert.ToInt32(finishHours[i].Split(":")[1]));
                    timeOfDay.Price = (expertInformation.IsFreeOfCharge == true) ? 0 : expertInformation.Price;

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
