using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Comments.Experts;
using CR.Core.DTOs.ResultDTOs.Comments;
using CR.Core.Services.Interfaces.Comments;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CR.Core.Services.Implementations.Comments
{
    public class GetExpertCommentsForAdminPanelService : IGetExpertCommentsForAdminPanelService
    {
        private readonly ApplicationContext _context;

        public GetExpertCommentsForAdminPanelService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetAllExpertCommentsForAdminPanelDto> Execute(int Page = 1, int PageSize = 20)
        {
            var expertInformations = _context.ExpertInformations.AsQueryable();

            var expertComments = _context.Comments
                .Include(_ => _.Rate)
                .Include(_ => _.User)
                .Where(_ => _.TypeId == CommentType.Expert)
                .Select(_ => new ExpertCommentForAdminPanelDto
                {
                    CommenterFullName = _.User.FirstName + " " + _.User.LastName,
                    CommenterIconSrc = _.User.IconSrc ?? "assets/img/icon-256x256.png",
                    CommenterId = _.User.Id,
                    CreateDate = _.CreateDate.ToShamsi(),
                    ExpertFullName = expertInformations.FirstOrDefault(e => e.Id == _.OwnerRecordId).FirstName + " " +
                                     expertInformations.FirstOrDefault(e => e.Id == _.OwnerRecordId).LastName,
                    ExpertId = expertInformations.FirstOrDefault(e => e.Id == _.OwnerRecordId).ExpertId,
                    ExpertIconSrc = expertInformations.FirstOrDefault(e => e.Id == _.OwnerRecordId).IconSrc,
                    Id = _.Id,
                    Message = (_.Message.Length > 15)
                        ? _.Message.Substring(0, Math.Min(_.Message.Length, 15)) + "..."
                        : _.Message,
                    Status = _.CommentStatus.GetDisplayName(),
                    ShowStatus = _.ShowInMainPage,
                    Rate = _.Rate.FirstOrDefault().Rate
                }).AsEnumerable()
                .ToPaged(Page, PageSize, out var rowsCount)
                .ToList();

            int notReadCommentsCount = _context.Comments.Count(_ => _.TypeId == CommentType.Expert && _.IsRead == false);

            return new ResultDto<ResultGetAllExpertCommentsForAdminPanelDto>()
            {
                Data = new ResultGetAllExpertCommentsForAdminPanelDto()
                {
                    ExpertCommentForAdminPanelDtos = expertComments,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowsCount,
                    NotReadComments = notReadCommentsCount
                },
                IsSuccess = true
            };
        }
    }
}
