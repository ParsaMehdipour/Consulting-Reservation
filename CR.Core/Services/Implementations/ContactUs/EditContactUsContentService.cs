using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.ContactUs;
using CR.Core.Services.Interfaces.ContactUs;
using CR.DataAccess.Context;
using System;

namespace CR.Core.Services.Implementations.ContactUs
{
    public class EditContactUsContentService : IEditContactUsContentService
    {
        private readonly ApplicationContext _context;

        public EditContactUsContentService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestEditContactUsContentDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var contactUsContent = _context.ContactUsContents.Find(request.id);

                if (contactUsContent == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "اطلاعات یافت نشد!!"
                    };
                }

                contactUsContent.FullContent = request.fullContent;

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "متن تماس با ما با موفقیت ویرایش شد"
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
