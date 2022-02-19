using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.Favorites;
using CR.Core.Services.Interfaces.Favorites;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.Favorites;
using CR.DataAccess.Entities.IndividualInformations;
using System;
using System.Linq;

namespace CR.Core.Services.Implementations.Favorites
{
    public class AddNewFavoriteService : IAddNewFavoriteService
    {
        private readonly ApplicationContext _context;

        public AddNewFavoriteService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestAddNewFavoriteDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var expertInformation = GetExpertInformation(request.expertInformationId);

                if (expertInformation == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "متخصص یافت نشد"
                    };
                }

                if (_context.Favorites.Any(_ =>
                        _.ExpertInformationId == request.expertInformationId && _.UserId == request.userId))
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "مشاور مورد نظر از قبل در لیست علاقه مندی های شما هست"
                    };
                }

                var favorite = new Favorite()
                {
                    ExpertInformation = expertInformation,
                    ExpertInformationId = expertInformation.Id,
                    UserId = request.userId,
                    User = _context.Users.Find(request.userId)
                };

                _context.Favorites.Add(favorite);

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "مشاور با موفقیت به لیست علاقه مندی ها افزوده شد"
                };
            }
            catch (Exception)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "خطا!!"
                };
            }
        }

        private ExpertInformation GetExpertInformation(long id)
        {
            return _context.ExpertInformations.Find(id);
        }
    }
}
