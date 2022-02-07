using System.Collections.Generic;
using CR.Core.DTOs.Factors;

namespace CR.Core.DTOs.ResultDTOs.Factors
{
    public class ResultGetAllFactorsForAdminPanelDto
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<FactorForAdminDto> FactorForAdminDtos { get; set; }
    }
}
