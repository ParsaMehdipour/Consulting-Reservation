using CR.Common.DTOs;
using CR.Core.DTOs.ResultDTOs.Favorites;

namespace CR.Core.Services.Interfaces.Favorites
{
    public interface IGetConsumerFavoritesService
    {
        ResultDto<ResultGetConsumerFavoritesDto> Execute(long userId, int Page = 1, int PageSize = 20);
    }
}
