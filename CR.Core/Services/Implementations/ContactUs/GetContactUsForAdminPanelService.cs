using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.ContactUs;
using CR.Core.DTOs.ResultDTOs.ContactUs;
using CR.Core.Services.Interfaces.ContactUs;
using CR.DataAccess.Context;
using System.Linq;

namespace CR.Core.Services.Implementations.ContactUs
{
    public class GetContactUsForAdminPanelService : IGetContactUsForAdminPanelService
    {
        private readonly ApplicationContext _context;

        public GetContactUsForAdminPanelService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetContactUsDto> Execute(int Page = 1, int PageSize = 20)
        {
            var contactUs = _context.ContactUs.Select(_ => new ContactUsDto()
            {
                id = _.Id,
                FullName = _.FullName,
                Email = _.Email,
                IsRead = _.IsRead,
                Message = _.Message,
                CreateDate = _.CreateDate.ToShamsi()
            }).AsEnumerable()
                .ToPaged(Page, PageSize, out var rowsCount)
                .ToList();

            return new ResultDto<ResultGetContactUsDto>()
            {
                Data = new ResultGetContactUsDto()
                {
                    ContactUsDtos = contactUs,
                    PageSize = PageSize,
                    CurrentPage = Page,
                    RowCount = rowsCount
                },
                IsSuccess = true
            };
        }
    }
}
