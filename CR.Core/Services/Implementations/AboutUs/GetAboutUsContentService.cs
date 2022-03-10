using CR.Common.DTOs;
using CR.Core.DTOs.AboutUs;
using CR.Core.Services.Interfaces.AboutUs;
using CR.DataAccess.Context;
using System.Linq;

namespace CR.Core.Services.Implementations.AboutUs
{
    public class GetAboutUsContentService : IGetAboutUsContentService
    {
        private readonly ApplicationContext _context;

        public GetAboutUsContentService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<AboutUsDto> Execute()
        {
            var aboutUs = _context.AboutUs.FirstOrDefault();

            if (aboutUs == null)
            {
                return new ResultDto<AboutUsDto>
                {
                    Data = null,
                    IsSuccess = false,
                };
            }

            var data = new AboutUsDto()
            {
                Id = aboutUs.Id,
                FullContent = aboutUs.FullContent
            };

            return new ResultDto<AboutUsDto>()
            {
                Data = data,
                IsSuccess = true
            };
        }
    }
}
