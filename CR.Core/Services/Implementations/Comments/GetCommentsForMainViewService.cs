using CR.Common.DTOs;
using CR.Core.DTOs.Comments;
using CR.Core.Services.Interfaces.Comments;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CR.Core.Services.Implementations.Comments
{
    public class GetCommentsForMainViewService : IGetCommentsForMainViewService
    {
        private readonly ApplicationContext _context;

        public GetCommentsForMainViewService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<List<CommentForMainViewDto>> Execute()
        {
            var comments = _context.Comments
                .Include(_ => _.User)
                .Where(_ => _.ShowInMainPage)
                .OrderByDescending(_ => _.CreateDate)
                .Take(10)
                .Select(_ => new CommentForMainViewDto()
                {
                    Message = _.Message,
                    FirstName = _.User.FirstName
                }).ToList();

            return new ResultDto<List<CommentForMainViewDto>>()
            {
                Data = comments,
                IsSuccess = true
            };
        }
    }
}
