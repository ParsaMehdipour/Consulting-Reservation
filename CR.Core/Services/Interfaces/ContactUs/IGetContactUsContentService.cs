using CR.Common.DTOs;
using CR.Core.DTOs.ContactUs;

namespace CR.Core.Services.Interfaces.ContactUs
{
    public interface IGetContactUsContentService
    {
        ResultDto<ContactUsContentDto> Execute();
    }
}
