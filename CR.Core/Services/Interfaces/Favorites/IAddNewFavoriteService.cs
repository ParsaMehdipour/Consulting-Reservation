using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.Favorites;

namespace CR.Core.Services.Interfaces.Favorites
{
    public interface IAddNewFavoriteService
    {
        ResultDto Execute(RequestAddNewFavoriteDto request);
    }
}
