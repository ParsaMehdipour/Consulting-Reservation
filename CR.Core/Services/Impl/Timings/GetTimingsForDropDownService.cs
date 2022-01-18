using System.Collections.Generic;
using System.Linq;
using CR.Common.DTOs;
using CR.Core.DTOs.Timings;
using CR.Core.Services.Interfaces.Timings;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;

namespace CR.Core.Services.Impl.Timings
{
    public class GetTimingsForDropDownService : IGetTimingsForDropDownService
    {
        private readonly ApplicationContext _context;

        public GetTimingsForDropDownService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<List<TimingDto>> Execute(TimingType timingType)
        {
            var data = new List<TimingDto>();

            if (timingType == TimingType.ShortSpan)
            {
                data = _context.Timings.Where(t => t.TimingType == TimingType.ShortSpan)
                    .Select(t => new TimingDto
                    {
                        Id = t.Id,
                        Start = t.StartTime_String,
                        End = t.EndTime_String,
                        TimingType = t.TimingType
                    }).ToList();
            }
            else if (timingType == TimingType.MediumSpan)
            {
                data = _context.Timings.Where(t => t.TimingType == TimingType.MediumSpan)
                    .Select(t => new TimingDto
                    {
                        Id = t.Id,
                        Start = t.StartTime_String,
                        End = t.EndTime_String,
                        TimingType = t.TimingType
                    }).ToList();
            }
            else
            {
                data = _context.Timings.Where(t => t.TimingType == TimingType.LongSpan)
                    .Select(t => new TimingDto
                    {
                        Id = t.Id,
                        Start = t.StartTime_String,
                        End = t.EndTime_String,
                        TimingType = t.TimingType
                    }).ToList();
            }

            return new ResultDto<List<TimingDto>>()
            {
                Data = data,
                IsSuccess = true
            };
        }
    }
}
