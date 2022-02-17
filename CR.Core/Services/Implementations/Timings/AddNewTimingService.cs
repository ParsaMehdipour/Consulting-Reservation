using System;
using System.Collections.Generic;
using System.Linq;
using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs;
using CR.Core.Services.Interfaces.Timings;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.Timings;

namespace CR.Core.Services.Implementations.Timings
{
    public class AddNewTimingService : IAddNewTimingService
    {
        private readonly ApplicationContext _context;

        public AddNewTimingService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestAddNewTimingDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                if (request.timings == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "لطفا یک زمانبندی ایجاد کنید!!"
                    };
                }

                var timingList = new List<Timing>();

                foreach (var timingDto in request.timings)
                {
                    var timing = new Timing()
                    {
                        StartTime_String = timingDto.startTime,
                        StartTime = DateTime.ParseExact($"{timingDto.startTime}:00,531", "HH:mm:ss,fff",
                            System.Globalization.CultureInfo.InvariantCulture),
                        EndTime_String = timingDto.endTime,
                        EndTime = DateTime.ParseExact($"{timingDto.endTime}:00,531", "HH:mm:ss,fff",
                            System.Globalization.CultureInfo.InvariantCulture),
                        TimingType = timingDto.timingType,
                    };

                    if (_context.Timings.Any(t => t.StartTime_String == timing.StartTime_String && t.EndTime_String == timing.EndTime_String && t.TimingType == timing.TimingType))
                    {
                        return new ResultDto()
                        {
                            IsSuccess = false,
                            Message = "در زمانبندی های شما تداخل وجود دارد"
                        };
                    }

                    timingList.Add(timing);

                }


                _context.Timings.AddRange(timingList);

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "تغیرات شما با موفقیت افزوده شد"
                };
            }
            catch (Exception)
            {
                transaction.Rollback();

                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "خطا از سمت سرور!!"
                };
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}
