using CR.Common.DTOs;
using CR.Core.DTOs.ResultDTOs.Comments;

namespace CR.Core.Services.Interfaces.Comments
{
    public interface IGetExpertCommentsForPresentationService
    {
        ResultDto<ResultGetExpertCommentsForPresentationDto> Execute(long expertInformationId);
    }
}
