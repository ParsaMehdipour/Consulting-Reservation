using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Timings;
using CR.Core.Services.Interfaces.Timings;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using System.Collections.Generic;
using System.Linq;

namespace CR.Core.Services.Implementations.Timings
{
    public class GetTimingsForDropDownService : IGetTimingsForDropDownService
    {
        private readonly ApplicationContext _context;

        public GetTimingsForDropDownService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<TimingForDropDownDtos> Execute(TimingType timingType)
        {
            var data = new List<TimingDto>();

            if (timingType == TimingType.ShortSpan)
            {
                data = _context.Timings.Where(t => t.TimingType == TimingType.ShortSpan).OrderBy(t => t.EndTime)
                    .Select(t => new TimingDto
                    {
                        id = t.Id,
                        startTime = t.StartTime_String,
                        endTime = t.EndTime_String,
                        timingType = t.TimingType
                    }).OrderBy(a => a.startTime).ToList();
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
                    }).OrderBy(a => a.startTime).ToList();
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
                    }).OrderBy(a => a.startTime).ToList();
            }

            return new ResultDto<TimingForDropDownDtos>()
            {
                Data = new TimingForDropDownDtos()
                {
                    timingDtos = data,
                    Label = timingType.GetDisplayName()
                },
                IsSuccess = true
            };
        }
    }
}
