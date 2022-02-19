using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.Favorites;

namespace CR.Core.Services.Interfaces.Favorites
{
    public interface IRemoveFromFavoritesListService
    {
        ResultDto Execute(RequestRemoveFormFavoritesListDto request);
    }
}
