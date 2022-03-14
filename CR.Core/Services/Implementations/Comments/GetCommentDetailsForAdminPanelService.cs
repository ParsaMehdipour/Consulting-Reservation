using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Comments;
using CR.Core.Services.Interfaces.Comments;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CR.Core.Services.Implementations.Comments
{
    public class GetCommentDetailsForAdminPanelService : IGetCommentDetailsForAdminPanelService
    {
        private readonly ApplicationContext _context;

        public GetCommentDetailsForAdminPanelService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<CommentDetailsForAdminPanelDto> Execute(long id)
        {
            var comment = _context.Comments.Include(_ => _.User).FirstOrDefault(_ => _.Id == id);

            if (comment == null)
            {
                return new ResultDto<CommentDetailsForAdminPanelDto>()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "نظر یافت نشد!!"
                };
            }

            comment.IsRead = true;

            _context.SaveChanges();

            var result = new CommentDetailsForAdminPanelDto()
            {
                commenterFullName = comment.User.FirstName + " " + comment.User.LastName,
                commentType = comment.TypeId,
                createDate = comment.CreateDate.ToShamsi(),
                message = comment.Message,
                status = comment.CommentStatus.GetDisplayName()
            };

            return new ResultDto<CommentDetailsForAdminPanelDto>()
            {
                Data = result,
                IsSuccess = true
            };
        }
    }
}
