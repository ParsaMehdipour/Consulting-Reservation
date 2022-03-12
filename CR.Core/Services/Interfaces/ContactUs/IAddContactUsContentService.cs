using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.ContactUs;

namespace CR.Core.Services.Interfaces.ContactUs
{
    public interface IAddContactUsContentService
    {
        ResultDto Execute(RequestAddNewContactUsContentDto request);
    }
}
