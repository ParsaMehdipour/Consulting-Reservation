using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.AboutUs;

namespace CR.Core.Services.Interfaces.AboutUs
{
    public interface IAddAboutUsService
    {
        ResultDto Execute(RequestAddAboutUsDto request);
    }
}
