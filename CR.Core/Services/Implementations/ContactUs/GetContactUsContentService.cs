using CR.Common.DTOs;
using CR.Core.DTOs.ContactUs;
using CR.Core.Services.Interfaces.ContactUs;
using CR.DataAccess.Context;
using System.Linq;

namespace CR.Core.Services.Implementations.ContactUs
{
    public class GetContactUsContentService : IGetContactUsContentService
    {
        private readonly ApplicationContext _context;

        public GetContactUsContentService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ContactUsContentDto> Execute()
        {
            var contactUsContent = _context.ContactUsContents.FirstOrDefault();

            if (contactUsContent == null)
            {
                return new ResultDto<ContactUsContentDto>()
                {
                    IsSuccess = true,
                    Message = "اطلاعات تماس با ما یافت نشد"
                };
            }

            var contactUsContentDto = new ContactUsContentDto()
            {
                Id = contactUsContent.Id,
                FullContent = contactUsContent.FullContent
            };

            return new ResultDto<ContactUsContentDto>()
            {
                Data = contactUsContentDto,
                IsSuccess = true
            };
        }
    }
}
