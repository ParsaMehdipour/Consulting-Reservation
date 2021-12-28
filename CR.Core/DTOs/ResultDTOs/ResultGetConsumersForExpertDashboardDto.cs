using System.Collections.Generic;
using CR.Core.DTOs.Consumers;

namespace CR.Core.DTOs.ResultDTOs
{
    public class ResultGetConsumersForExpertDashboardDto
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<ConsumerForExpertDashboardDto> ConsumerForExpertDashboardDtos { get; set; }
    }
}
