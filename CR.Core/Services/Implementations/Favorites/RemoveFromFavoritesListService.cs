using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.Favorites;
using CR.Core.Services.Interfaces.Favorites;
using CR.DataAccess.Context;
using System;
using System.Linq;

namespace CR.Core.Services.Implementations.Favorites
{
    public class RemoveFromFavoritesListService : IRemoveFromFavoritesListService
    {
        private readonly ApplicationContext _context;

        public RemoveFromFavoritesListService(ApplicationContext context)
        {
            _context = context;
        }


        public ResultDto Execute(RequestRemoveFormFavoritesListDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var favorite = _context.Favorites.FirstOrDefault(_ => _.UserId == request.userId && _.ExpertInformationId == request.expertInformationId);

                if (favorite == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "علاقه مندی یافت نشد!!"
                    };
                }

                _context.Favorites.Remove(favorite);

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "مشاور از لیست علاقه مندی های شما حذف شد"
                };
            }
            catch (Exception)
            {
                transaction.Rollback();

                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "خطا!!"
                };
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}
