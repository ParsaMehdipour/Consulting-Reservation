using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Comments.Experts;
using CR.Core.DTOs.ResultDTOs.Comments;
using CR.Core.Services.Interfaces.Comments;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using System.Collections.Generic;
using System.Linq;

namespace CR.Core.Services.Implementations.Comments
{
    public class GetExpertCommentsForExpertPanelService : IGetExpertCommentsForExpertPanelService
    {
        private readonly ApplicationContext _context;

        public GetExpertCommentsForExpertPanelService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetExpertCommentsForExpertPanelDto> Execute(long expertId, int page = 1, int pageSize = 20)
        {
            var expertInformationId = _context.ExpertInformations.FirstOrDefault(_ => _.ExpertId == expertId);

            if (expertInformationId == null)
            {
                return new ResultDto<ResultGetExpertCommentsForExpertPanelDto>()
                {
                    Data = new ResultGetExpertCommentsForExpertPanelDto(),
                    IsSuccess = false,
                    Message = "خطا!!"
                };
            }

            var expertComments = _context.Comments
                .Where(_ => _.TypeId == CommentType.Expert
                            && _.OwnerRecordId == expertInformationId.Id
                            && _.CommentStatus == CommentStatus.Accepted
                            && _.ParentId == null)
                .Select(_ => new ExpertCommentDto
                {
                    CommenterFullName = (_.User.FirstName != null) ? _.User.FirstName + " " + _.User.LastName : "بی نام",
                    CommenterIconSrc = _.User.IconSrc ?? "assets/img/icon-256x256.png",
                    CreateDate = _.CreateDate.ToShamsi(),
                    Id = _.Id,
                    Message = _.Message,
                    HasChildren = _.Children.Any(c => c.CommentStatus == CommentStatus.Accepted),
                    Children = _.Children.Any(c => c.CommentStatus == CommentStatus.Accepted) ? _.Children.Select(c => new ExpertCommentDto
                    {
                        ParentId = c.ParentId.Value,
                        CommenterFullName = c.User.FirstName + " " + c.User.LastName,
                        CommenterIconSrc = c.User.IconSrc ?? "assets/img/icon-256x256.png",
                        CreateDate = c.CreateDate.ToShamsi(),
                        Id = c.Id,
                        Message = c.Message
                    }).ToList() : new List<ExpertCommentDto>()
                }).AsEnumerable()
                .ToPaged(page, pageSize, out var rowsCount)
                .ToList();

            return new ResultDto<ResultGetExpertCommentsForExpertPanelDto>()
            {
                Data = new ResultGetExpertCommentsForExpertPanelDto()
                {
                    ExpertCommentDtos = expertComments,
                    CurrentPage = page,
                    PageSize = pageSize,
                    RowCount = rowsCount
                },
                IsSuccess = true
            };
        }
    }
}
