using System.Collections.Generic;
using CR.Common.DTOs;
using CR.Core.DTOs.Specialities;

namespace CR.Core.Services.Interfaces.Specialites
{
    public interface IGetSpecialtiesForExpertProfileDropDownService
    {
        ResultDto<List<SpecialityDto>> Execute();
    }
}
