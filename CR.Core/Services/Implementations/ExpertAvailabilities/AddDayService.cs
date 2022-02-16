using System;
using System.Linq;
using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs;
using CR.Core.Services.Interfaces.ExpertAvailabilities;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.ExpertAvailabilities;

namespace CR.Core.Services.Implementations.ExpertAvailabilities
{
    public class AddDayService : IAddDayService
    {
        private readonly ApplicationContext _context;

        public AddDayService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestAddNewDayDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {

                var expertInformation =
                    _context.ExpertInformations.FirstOrDefault(e => e.ExpertId == request.expertId);

                if (expertInformation == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "اطلاعات متخصص یافت نشد !!"
                    };
                }

                if (_context.Days.Any(d =>
                        d.Date_String == request.date && d.ExpertInformationId == expertInformation.Id))
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "این تاریخ قبلا اضافه شده است"
                    };
                }

                var day = new Day()
                {
                    Date = request.date.ToGeorgianDateTime(),
                    Date_String = request.date,
                    ExpertInformationId = expertInformation.Id,
                    ExpertInformation = expertInformation,
                    DayOfWeek = (int)request.date.ToGeorgianDateTime().DayOfWeek
                };

                _context.Days.Add(day);

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "روز با موفقیت افزوده شد"
                };
            }
            catch (Exception)
            {
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
