using System.Linq;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.ResultDTOs;
using CR.Core.DTOs.Timings;
using CR.Core.Services.Interfaces.Timings;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;

namespace CR.Core.Services.Impl.Timings
{
    public class GetAllTimingsForAdminService : IGetAllTimingsForAdminService
    {
        private readonly ApplicationContext _context;

        public GetAllTimingsForAdminService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetAllTimingsForAdminPanelDto> Execute(int Page = 1, int PageSize = 20)
        {
            var timings = _context.Timings
                .OrderByDescending(t=>t.TimingType)
                .Select(t => new TimingDto()
                {
                    Start = t.StartTime_String,
                    End = t.EndTime_String,
                    TimingType = t.TimingType,
                    Id = t.Id
                })
                .AsEnumerable()
                .ToPaged(Page, PageSize, out var rowCount)
                .ToList();


            return new ResultDto<ResultGetAllTimingsForAdminPanelDto>()
            {
                Data = new ResultGetAllTimingsForAdminPanelDto()
                {
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount,
                    TimingDtos = timings,
                },

            };
        }
    }
}
