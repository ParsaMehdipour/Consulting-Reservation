using CR.Common.DTOs;
using CR.Core.Services.Interfaces.Consumers;
using CR.DataAccess.Context;

namespace CR.Core.Services.Implementations.Comments
{
    public class ShowCommentInMainPageService : IShowCommentInMainPageService
    {
        private readonly ApplicationContext _context;

        public ShowCommentInMainPageService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long commentId, bool showStatus)
        {
            var comment = _context.Comments.Find(commentId);

            if (comment == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "نظر مورد نظر یافت نشد"
                };
            }

            comment.ShowInMainPage = showStatus;

            _context.SaveChanges();

            if (showStatus)
            {
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "نظر با موفقیت به صفحه اصلی انتقال داده شد"
                };
            }

            return new ResultDto()
            {
                IsSuccess = true,
                Message = "نظر با موفقیت از صفحه اصلی ورداشته شد"
            };
        }
    }
}
