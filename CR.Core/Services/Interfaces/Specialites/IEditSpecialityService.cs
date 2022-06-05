using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.Specialty;

namespace CR.Core.Services.Interfaces.Specialites
{
    public interface IEditSpecialityService
    {
        ResultDto Execute(RequestEditSpecialityDto request);
    }
}
