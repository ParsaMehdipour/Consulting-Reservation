using CR.Common.DTOs;
using CR.Core.DTOs.Timings;
using CR.Core.Services.Interfaces.Timings;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using System.Linq;

namespace CR.Core.Services.Implementations.Timings
{
    public class GetAvailableTimingsService : IGetAvailableTimingsService
    {
        private readonly ApplicationContext _context;

        public GetAvailableTimingsService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<AvailableTimingDto> Execute()
        {

            var timings = _context.Timings.ToList();

            return new ResultDto<AvailableTimingDto>()
            {
                Data = new AvailableTimingDto()
                {
                    isShortSpan = timings.Any(_ => _.TimingType == TimingType.ShortSpan),
                    isMediumSpan = timings.Any(_ => _.TimingType == TimingType.MediumSpan),
                    isLongSpan = timings.Any(_ => _.TimingType == TimingType.LongSpan)
                },
                IsSuccess = true
            };
        }
    }
}
