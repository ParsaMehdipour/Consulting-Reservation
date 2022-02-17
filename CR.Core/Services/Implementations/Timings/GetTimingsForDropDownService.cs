using System.Collections.Generic;
using System.Linq;
using CR.Common.DTOs;
using CR.Core.DTOs.Timings;
using CR.Core.Services.Interfaces.Timings;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;

namespace CR.Core.Services.Implementations.Timings
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
                data = _context.Timings.Where(t => t.TimingType == TimingType.ShortSpan).OrderBy(t=>t.EndTime)
                    .Select(t => new TimingDto
                    {
                        id = t.Id,
                        startTime = t.StartTime_String,
                        endTime = t.EndTime_String,
                        timingType = t.TimingType
                    }).ToList();
            }
            else if (timingType == TimingType.MediumSpan)
            {
                data = _context.Timings.Where(t => t.TimingType == TimingType.MediumSpan).OrderBy(t => t.EndTime)
                    .Select(t => new TimingDto
                    {
                        id = t.Id,
                        startTime = t.StartTime_String,
                        endTime = t.EndTime_String,
                        timingType = t.TimingType
                    }).ToList();
            }
            else
            {
                data = _context.Timings.Where(t => t.TimingType == TimingType.LongSpan).OrderBy(t => t.EndTime)
                    .Select(t => new TimingDto
                    {
                        id = t.Id,
                        startTime = t.StartTime_String,
                        endTime = t.EndTime_String,
                        timingType = t.TimingType
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
