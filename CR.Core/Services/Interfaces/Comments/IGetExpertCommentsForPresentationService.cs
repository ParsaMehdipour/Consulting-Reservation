using CR.Common.DTOs;
using CR.Core.DTOs.ResultDTOs.Comments;

namespace CR.Core.Services.Interfaces.Comments
{
    public interface IGetExpertCommentsService
    {
        ResultDto<ResultGetExpertCommentsDto> Execute(long expertInformationId);
    }
}
