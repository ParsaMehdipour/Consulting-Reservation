using CR.Core.DTOs.Experts;
using System.Collections.Generic;

namespace CR.Core.DTOs.ResultDTOs.Favorites
{
    public class ResultGetConsumerFavoritesDto
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<ExpertForPresentationDto> ConsumerFavorites { get; set; }
    }
}
