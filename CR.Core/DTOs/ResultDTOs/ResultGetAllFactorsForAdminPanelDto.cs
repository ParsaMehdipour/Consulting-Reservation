using CR.Core.DTOs.Factors;
using System.Collections.Generic;

namespace CR.Core.DTOs.ResultDTOs
{
    public class ResultGetAllFactorsForAdminPanelDto
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<FactorForAdminDto> FactorForAdminDtos { get; set; }
    }
}
