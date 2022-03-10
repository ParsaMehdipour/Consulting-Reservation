using CR.Common.DTOs;
using CR.Core.DTOs.AboutUs;

namespace CR.Core.Services.Interfaces.AboutUs
{
    public interface IGetAboutUsContentService
    {
        ResultDto<AboutUsDto> Execute();
    }
}
