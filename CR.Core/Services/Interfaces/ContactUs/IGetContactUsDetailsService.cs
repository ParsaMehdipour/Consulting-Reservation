using CR.Common.DTOs;
using CR.Core.DTOs.ContactUs;

namespace CR.Core.Services.Interfaces.ContactUs
{
    public interface IGetContactUsDetailsService
    {
        ResultDto<ContactUsDetailsForAdminPanel> Execute(long id);
    }
}
