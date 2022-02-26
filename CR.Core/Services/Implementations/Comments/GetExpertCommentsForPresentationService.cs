using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Core.DTOs.Comments.Experts;
using CR.Core.DTOs.ResultDTOs.Comments;
using CR.Core.Services.Interfaces.Comments;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using System.Collections.Generic;
using System.Linq;

namespace CR.Core.Services.Implementations.Comments
{
    public class GetExpertCommentsForPresentationService : IGetExpertCommentsForPresentationService
    {
        private readonly ApplicationContext _context;

        public GetExpertCommentsForPresentationService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetExpertCommentsForPresentationDto> Execute(long expertInformationId)
        {
            var expertComments = _context.Comments
                .Where(_ => _.TypeId == CommentType.Expert
                            && _.OwnerRecordId == expertInformationId
                            && _.CommentStatus == CommentStatus.Accepted)
                .Select(_ => new ExpertCommentForPresentationDto
                {
                    CommenterFullName = (_.User.FirstName != null) ? _.User.FirstName + " " + _.User.LastName : "بی نام",
                    CommenterIconSrc = _.User.IconSrc ?? "assets/img/icon-256x256.png",
                    CreateDate = _.CreateDate.ToShamsi(),
                    Id = _.Id,
                    Message = _.Message,
                    HasChildren = _.Children.Any(c => c.CommentStatus == CommentStatus.Accepted),
                    Children = _.Children.Any(c => c.CommentStatus == CommentStatus.Accepted) ? _.Children.Select(c => new ExpertCommentForPresentationDto
                    {
                        ParentId = c.ParentId.Value,
                        CommenterFullName = c.User.FirstName + " " + c.User.LastName,
                        CommenterIconSrc = c.User.IconSrc ?? "assets/img/icon-256x256.png",
                        CreateDate = c.CreateDate.ToShamsi(),
                        Id = c.Id,
                        Message = c.Message
                    }).ToList() : new List<ExpertCommentForPresentationDto>()
                }).ToList();

            return new ResultDto<ResultGetExpertCommentsForPresentationDto>()
            {
                Data = new ResultGetExpertCommentsForPresentationDto()
                {
                    ExpertCommentForPresentationDtos = expertComments
                },
                IsSuccess = true
            };
        }
    }
}
