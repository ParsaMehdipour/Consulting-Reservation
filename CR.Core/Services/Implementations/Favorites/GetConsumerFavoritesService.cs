using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Experts;
using CR.Core.DTOs.ResultDTOs.Favorites;
using CR.Core.Services.Interfaces.Favorites;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CR.Core.Services.Implementations.Favorites
{
    public class GetConsumerFavoritesService : IGetConsumerFavoritesService
    {
        private readonly ApplicationContext _context;

        public GetConsumerFavoritesService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetConsumerFavoritesDto> Execute(long userId, int Page = 1, int PageSize = 20)
        {
            var consumerFavorites = _context.Favorites
                .Include(_ => _.ExpertInformation)
                .ThenInclude(_ => _.Specialty)
                .Where(_ => _.UserId == userId)
                .Select(_ => new ExpertForPresentationDto
                {
                    ExpertInformationId = _.ExpertInformation.Id,
                    FullName = _.ExpertInformation.FirstName + " " + _.ExpertInformation.LastName,
                    IconSrc = _.ExpertInformation.IconSrc,
                    RateCount = _context.Comments.Count(c => c.TypeId == CommentType.Expert && c.CommentStatus == CommentStatus.Accepted && c.OwnerRecordId == _.ExpertInformationId),
                    AverageRate = Decimal.Round(_.ExpertInformation.AverageRate),
                    HasStar = (_.ExpertInformation.Favorites.Count >= 2),
                    Id = _.ExpertInformation.ExpertId,
                    Speciality = _.ExpertInformation.Specialty.Name,
                    SpecialitySrc = _.ExpertInformation.Specialty.ImageSrc,
                    Tags = _.ExpertInformation.Tag
                }).AsEnumerable()
                .ToPaged(Page, PageSize, out var rowCount)
                .ToList();

            return new ResultDto<ResultGetConsumerFavoritesDto>()
            {
                Data = new ResultGetConsumerFavoritesDto()
                {
                    ConsumerFavorites = consumerFavorites,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount
                },
                IsSuccess = true
            };
        }
    }
}
