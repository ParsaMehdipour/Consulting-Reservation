using CR.Common.DTOs;
using CR.Core.DTOs.ResultDTOs;
using CR.Core.DTOs.Specialities;
using CR.Core.Services.Interfaces.Specialites;
using CR.DataAccess.Context;
using System.Linq;
using CR.Common.Utilities;
using CR.Core.DTOs.ResultDTOs.Specialities;

namespace CR.Core.Services.Impl.Specialites
{
    public class GetAllSpecialitiesService : IGetAllSpecialitiesService
    {
        private readonly ApplicationContext _context;

        public GetAllSpecialitiesService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetAllSpecialitiesDto> Execute(int Page = 1, int PageSize = 20)
        {
            int rowCount = 0;
            var specialities = _context.Specialties.Select(s=> new SpecialityDto
            {
                Id = s.Id,
                Name = s.Name,
                ImageSrc = s.ImageSrc,
            }).OrderByDescending(s=>s.Id)
                .ToPaged(Page, PageSize, out rowCount)
                .ToList();

            return new ResultDto<ResultGetAllSpecialitiesDto>
            {
                Data = new ResultGetAllSpecialitiesDto
                {
                    Specialities = specialities,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount,
                },
                IsSuccess = true,
            };

        }
    }
}
