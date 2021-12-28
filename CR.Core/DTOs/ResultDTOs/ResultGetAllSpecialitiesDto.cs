using CR.Core.DTOs.Specialities;
using System.Collections.Generic;

namespace CR.Core.DTOs.ResultDTOs
{
    public class ResultGetAllSpecialitiesDto
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<SpecialityDto> Specialities { get; set; }
    }
}
