using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs;
using CR.Core.DTOs.RequestDTOs.Specialty;
using CR.Core.DTOs.ResultDTOs;
using CR.Core.DTOs.ResultDTOs.Specialities;

namespace CR.Core.Services.Interfaces.Specialites
{
    public interface IGetAllSpecialitiesService
    {
        ResultDto<ResultGetAllSpecialitiesDto> Execute(int Page = 1, int PageSize = 20, int parentId = 0);
        ResultDto<RequestEditSpecialityDto> GetSpecialty(long id);
        ResultDto EditSpecialty(RequestEditSpecialityDto request);
        ResultDto DeleteSpecialty(long SpecialtyId);
        ResultDto AddNewSpecialty(RequestAddNewSpecialtyDto request);
        Select2Response GetAllAttributeTypeForSelect2(Select2Request model);
        Select2Response GetSelect2ItemsForExpert(long expertid);
    }
}