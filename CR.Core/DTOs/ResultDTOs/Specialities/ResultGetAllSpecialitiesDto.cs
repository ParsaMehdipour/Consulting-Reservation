using System.Collections.Generic;
using CR.Core.DTOs.Specialities;

namespace CR.Core.DTOs.ResultDTOs.Specialities
{
    public class ResultGetAllSpecialitiesDto
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<SpecialityDto> Specialities { get; set; }
    }
}
