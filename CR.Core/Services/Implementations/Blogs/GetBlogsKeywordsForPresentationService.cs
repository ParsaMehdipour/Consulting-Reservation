using CR.Common.DTOs;
using CR.Core.Services.Interfaces.Blogs;
using CR.DataAccess.Context;
using System.Collections.Generic;
using System.Linq;

namespace CR.Core.Services.Implementations.Blogs
{
    public class GetBlogsKeywordsForPresentationService : IGetBlogsKeywordsForPresentationService
    {
        private readonly ApplicationContext _context;

        public GetBlogsKeywordsForPresentationService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<List<string>> Execute()
        {
            var blogs = _context.Blogs.OrderByDescending(_ => _.PublishDate).Take(4);

            var list = new List<string>();

            foreach (var blog in blogs)
            {
                list.AddRange(blog.Keywords.Split(",").ToList());
            }

            return new ResultDto<List<string>>()
            {
                Data = list,
                IsSuccess = true
            };
        }
    }
}
