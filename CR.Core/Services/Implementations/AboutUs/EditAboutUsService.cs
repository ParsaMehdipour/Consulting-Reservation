using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.AboutUs;
using CR.Core.Services.Interfaces.AboutUs;
using CR.DataAccess.Context;
using System;

namespace CR.Core.Services.Implementations.AboutUs
{
    public class EditAboutUsService : IEditAboutUsService
    {
        private readonly ApplicationContext _context;

        public EditAboutUsService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestEditAboutUsDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var aboutUs = _context.AboutUs.Find(request.id);

                if (aboutUs == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "اطلاعات یافت نشد!!"
                    };
                }

                aboutUs.FullContent = request.fullContent;

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "درباره ما با موفقیت ویرایش شد"
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
        }
    }
}
