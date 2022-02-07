using System.Collections.Generic;
using CR.Core.DTOs.Consumers;

namespace CR.Core.DTOs.ResultDTOs.Consumers
{
    public class ResultGetConsumersForExpertPanelDto
    {
        public List<ConsumerForExpertPanelDto> ConsumerForExpertPanelDtos { get; set; }
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
