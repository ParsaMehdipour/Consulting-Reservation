using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs;

namespace CR.Core.Services.Interfaces.Specialites
{
    public interface IAddNewSpecialityService
    {
        ResultDto Execute(RequestAddNewSpecialtyDto request);
    }
}
