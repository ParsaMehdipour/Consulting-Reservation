using CR.Core.DTOs.Consumers;
using System.Collections.Generic;

namespace CR.Core.DTOs.ResultDTOs
{
    public class ResultGetAllConsumersDto
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<ConsumerForAdminDto> Consumers { get; set; }
    }
}
