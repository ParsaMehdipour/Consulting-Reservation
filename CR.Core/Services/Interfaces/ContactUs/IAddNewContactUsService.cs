using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.ContactUs;

namespace CR.Core.Services.Interfaces.ContactUs
{
    public interface IAddNewContactUsService
    {
        ResultDto Execute(RequestAddNewContactUsDto request);
    }
}
